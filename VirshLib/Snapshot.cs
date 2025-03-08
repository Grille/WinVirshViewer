using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirshLib;

public class Snapshot : RemoteObject, IHasName
{
    public string Name { get; init; }
    public override VirtualMachine Owner => (VirtualMachine)base.Owner;
    public string ParentName { get; init; }
    public IReadOnlyCollection<Snapshot> Children { get; set; }
    public string Description { get; init; }
    public DateTime CreationTime { get; init; }
    public VmState State { get; init; }

    public Snapshot(VirtualMachine owner) : base(owner)
    {
        Children = new List<Snapshot>();
    }

    public Task<SshCommandResult> Revert()
    {
        return Owner.SnapshotList.Revert(Name);
    }

    public Task<SshCommandResult> Delete()
    {
        return Owner.SnapshotList.Delete(Name);
    }

    protected override void OnPull()
    {
        throw new NotSupportedException();
    }
}
