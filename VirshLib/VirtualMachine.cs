using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VirshLib;

public class VirtualMachine : RemoteObject, IHasName
{
    public int ID { get; internal set; }
    public string Name { get; internal set; }
    public VmState State { get; internal set; }

    public string Description { get; private set; }

    public string UUID { get; private set; }
    public string OsType { get; private set; }
    public int CpuCount { get; private set; }
    public int CpuTime { get; private set; }
    public int MaxMemory { get; private set; }
    public int UsedMemory { get; private set; }
    public bool Persistent { get; private set; }
    public bool Autostart { get; private set; }
    public bool ManagedSave { get; private set; }

    public string SecurityModel { get; private set; }
    public int SecurityDoi { get; private set; }
    public string SecurityLabel { get; private set; }

    public string XmlSource { get; private set; }

    public VirtualMachineStats Stats { get; }

    public SnapshotList SnapshotList { get; }

    public override VirshHost Owner => (VirshHost)base.Owner;

    public VirtualMachine(VirshHost owner, int id, string name, VmState state): base(owner)
    {
        ID = id;
        Name = name;
        State = state;

        Stats = new VirtualMachineStats(this);
        SnapshotList = new SnapshotList(this);
    }

    public void UpdateDomainInfo(string name, VmState state)
    {
        Name = name;
        State = state;
    }

    private Task<SshCommandResult> RunCommandAndAwaitState(string cmd, VmState state)
    {
        var task = Task.Run(async () => {
            var result = await Connection.MainShell.Command(cmd);
            if (result.TryParseErrors(out string _))
                return result;

            await AwaitVmState(state, TimeSpan.FromSeconds(60));

            return result;
        });

        RegisterPendingTask(task);

        return task;
    }


    public Task<SshCommandResult> Start()
    {
        return RunCommandAndAwaitState($"virsh start \"{Name}\"", VmState.Running);
    }

    public Task<SshCommandResult> Reboot()
    {
        return RunCommandAndAwaitState($"virsh reboot \"{Name}\"", VmState.Running);
    }

    public Task<SshCommandResult> Resume()
    {
        return RunCommandAndAwaitState($"virsh resume \"{Name}\"", VmState.Running);
    }

    public Task<SshCommandResult> Suspend()
    {
        return RunCommandAndAwaitState($"virsh suspend \"{Name}\"", VmState.Paused);
    }

    public Task<SshCommandResult> Shutdown()
    {
        return RunCommandAndAwaitState($"virsh shutdown \"{Name}\"", VmState.ShutOff);
    }

    public Task<SshCommandResult> Reset()
    {
        return RunCommandAndAwaitState($"virsh reset \"{Name}\"", VmState.Running);
    }

    public Task<SshCommandResult> Destroy()
    {
        return RunCommandAndAwaitState($"virsh destroy \"{Name}\"", VmState.ShutOff);
    }

    public Task<SshCommandResult> Screenshot()
    {
        return Connection.MainShell.Command($"virsh screenshot \"{Name}\"");
    }

    public Task<SshCommandResult> VncDisplayAddress()
    {
        return Connection.MainShell.Command($"virsh vncdisplay \"{Name}\"");
    }

    public async Task<bool> AwaitVmState(VmState state, TimeSpan timeout)
    {
        var timeoutTime = DateTime.Now + timeout;
        while (true) {
            if (DateTime.Now - LastPullDate > TimeSpan.FromMilliseconds(1000))
                await Pull();
            if (State == state)
                return true;
            else if (DateTime.Now > timeoutTime)
                return false;
            await Task.Delay(500);
        }
    }

    public Task<SshCommandResult> Dumpxml()
    {
        return Connection.InfoShell.Command($"virsh dumpxml \"{Name}\"");
    }

    protected override void OnPull()
    {
        var dominfoProm = Connection.InfoShell.Command($"virsh dominfo \"{Name}\"");
        var dumpxmlProm = Connection.InfoShell.Command($"virsh dumpxml \"{Name}\"");

        var dominfo = dominfoProm.Result;

        var lines = dominfo;

        foreach (var line in lines) {
            var split = line.Split(':', 2);
            if (split.Length != 2)
                continue;

            string key = split[0].Trim().ToLower();
            string value = split[1].Trim();

            switch (key) {
                case "id":
                    ID = value == "-" ? -1 : int.Parse(value);
                    break;
                case "name":
                    Name = value;
                    break;
                case "uuid":
                    UUID = value;
                    break;
                case "os type":
                    OsType = value;
                    break;
                case "state":
                    State = value.ConvertToVmState();
                    break;

                case "cpu(s)":
                    CpuCount = int.Parse(value);
                    break;
                case "cpu time":
                    CpuTime = (int)float.Parse(value.Split("s")[0]);
                    break;
                case "max memory":
                    MaxMemory = int.Parse(value.Split(" KiB")[0]);
                    break;
                case "used memory":
                    UsedMemory = int.Parse(value.Split(" KiB")[0]);
                    break;
                case "persistent":
                    Persistent = value == "yes";
                    break;
                case "autostart":
                    Autostart = value == "enable";
                    break;
                case "managed save":
                    ManagedSave = value == "yes";
                    break;

                case "security model":
                    SecurityModel = value;
                    break;

                case "security doi":
                    SecurityDoi = int.Parse(value);
                    break;

                case "security label":
                    SecurityLabel = value;
                    break;
            }
        }

        var dumpxml = dumpxmlProm.Result;

        XmlSource = dumpxml.Content;

        //Stats.Pull().Wait();

    }
}