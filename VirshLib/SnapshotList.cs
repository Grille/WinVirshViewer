using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static VirshLib.LiveDataGraph;

namespace VirshLib;

public class SnapshotList : RemoteList<Snapshot>
{
    const string EmptyParentName = "\0";

    public override VirtualMachine Owner => (VirtualMachine)base.Owner;

    public Snapshot Current { get; private set; }

    public IReadOnlyCollection<Snapshot> Root { get; private set; }

    public SnapshotList(VirtualMachine owner) : base(owner)
    {
        Root = new List<Snapshot>();
    }

    protected override void OnPull()
    {
        var listQuery = Connection.InfoShell.Command($"virsh snapshot-list \"{Owner.Name}\" --parent");
        var currentQuery = Connection.InfoShell.Command($"virsh snapshot-current \"{Owner.Name}\" --name");

        var entries = Parse(listQuery.Result);

        //FindAndRemove((item) => entries.Find((info) => info.Name == item.Name) == null);

        var items = new List<Snapshot>();

        foreach (var entry in entries) {
            var snapshot = GetByName(entry.Name);
            if (entry.Equals(snapshot)) {
                items.Add(snapshot);
            }
            else {
                items.Add(entry.ToSnapshot(Owner));
            }
        }


        var childLists = new Dictionary<string, List<Snapshot>>() {
            { EmptyParentName, new() }
        };

        foreach (var item in items) {
            childLists.Add(item.Name, new List<Snapshot>());
        }

        foreach (var item in items) {
            childLists[item.ParentName].Add(item);
        }

        foreach (var item in items) {
            var list = childLists[item.Name];
            list.Sort((a, b) => -a.CreationTime.CompareTo(b.CreationTime));
            item.Children = list;
        }

        SetItems(items);
        Root = childLists[EmptyParentName];
        Current = GetByName(ParseCurrent(currentQuery.Result));
    }

    public Task<SshCommandResult> Create(string name)
    {
        var cmd = Connection.MainShell.Command($"virsh snapshot-create-as --domain \"{Owner.Name}\" --name \"{name}\"");
        RegisterPendingTask(cmd, name);
        return cmd;
    }

    public Task<SshCommandResult> Revert(string name)
    {
        var cmd = Connection.MainShell.Command($"virsh snapshot-revert --domain \"{Owner.Name}\" --snapshotname \"{name}\"");
        GetByName(name).RegisterPendingTask(cmd);
        return cmd;
    }

    public Task<SshCommandResult> Delete(string name)
    {
        var cmd = Connection.MainShell.Command($"virsh snapshot-delete --domain \"{Owner.Name}\" --snapshotname \"{name}\"");
        GetByName(name).RegisterPendingTask(cmd);
        return cmd;
    }

    public record class ParsedSnapshotEntry(string Name, DateTime CreationTime, VmState State, string Parent)
    {
        public bool Equals(Snapshot snapshot)
        {
            if (snapshot == null) 
                return false;
            return 
                snapshot.Name == Name && 
                snapshot.CreationTime == CreationTime && 
                snapshot.State == State && 
                snapshot.ParentName == Parent;
        }

        public Snapshot ToSnapshot(VirtualMachine owner)
        {
            return new Snapshot(owner) {
                Name = Name,
                CreationTime = CreationTime,
                State = State,
                ParentName = Parent,
            };
        }
    }

    public static List<ParsedSnapshotEntry> Parse(SshCommandResult result)
    {
        var list = new List<ParsedSnapshotEntry>();

        var lines = result;

        for (int i = 3; i < lines.Count - 1; i++) {
            var args = lines[i].Split(new[] { "   " }, StringSplitOptions.RemoveEmptyEntries);

            string name = args[0].Trim();
            DateTime time = DateTime.Parse(args[1]);
            VmState state = args[2].ConvertToVmState();
            string parent = args.Length > 3 ? args[3].Trim() : EmptyParentName;

            list.Add(new(name, time, state, parent));
        }

        return list;
    }

    public static string? ParseCurrent(SshCommandResult result)
    {
        var lines = result;
        return lines.Count > 1 ? lines[1].Trim() : null;
    }
}
