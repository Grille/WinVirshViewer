using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirshLib;

public class VirtualMachineList : RemoteList<VirtualMachine>
{
    public override VirshHost Owner => (VirshHost)base.Owner;

    public bool Error {  get; private set; }

    public VirtualMachineList(VirshHost owner) : base(owner)
    {
    }

    protected override void OnPull()
    {
        var result = Connection.InfoShell.Command("virsh list --all --table").Result;
        var table = ParseVmTable(result);

        Error = table.Error;
        if (Error) {
            return;
        }

        //FindAndRemove((item) => entries.Find((info) => info.Name == item.Name) == null);

        var items = new List<VirtualMachine>();

        foreach (var entry in table.List) {
            var vm = GetByName(entry.Name);
            if (vm == null) {
                //Console.WriteLine($"add {entry.Name} to {entries.Count}");

                vm = new VirtualMachine(Owner, entry.Id, entry.Name, entry.State);
                items.Add(vm);
            }
            else {
                vm.ID = entry.Id;
                vm.Name = entry.Name;
                vm.State = entry.State;
                items.Add(vm);
            }
        }

        items.Sort((VirtualMachine a, VirtualMachine b) => a.Name.CompareTo(b.Name));

        SetItems(items);
    }

    record class ParsedVmTableEntry(int Id, string Name, VmState State);

    record class ParsedVmTable(List<ParsedVmTableEntry> List, bool Error);

    static ParsedVmTable ParseVmTable(SshCommandResult result)
    {
        var list = new List<ParsedVmTableEntry>();

        var lines = result;

        //if (lines[1] != " Id   Name   State") {
        //    return new(list, true);
        //}

        for (int i = 2; i < lines.Count; i++) {
            var args = lines[i].Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length != 3) {
                continue;
            }

            int id = args[0] == "-" ? -1 : int.Parse(args[0]);
            string name = args[1];
            VmState state = args[2].ConvertToVmState();

            list.Add(new(id, name, state));
        }

        return new(list, false);
    }

    public Task<SshCommandResult> Create(string name)
    {
        var cmd = Connection.MainShell.Command($"virt-install --name \"{name}\"");
        RegisterPendingTask(cmd, name);
        return cmd;
    }

}
