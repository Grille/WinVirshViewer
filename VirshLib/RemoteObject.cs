using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirshLib;

public abstract class RemoteObject
{
    object _pullLock = new();

    public virtual RemoteObject Owner { get; }

    public SshConnection Connection { get; }

    public DateTime LastPullDate { get; private set; }

    public TimeSpan DeltaSinceLastPull => DateTime.Now - LastPullDate;

    public bool PullSemaphore { get; private set; }

    public int DiscardedPullCount { get; private set; }

    public int PendingTaskCount { get; private set; }

    //public event EventHandler<RemoteObject> RemoteObjectChanged;

    public RemoteObject(SshConnection connection)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));

        Connection = connection;
    }

    public RemoteObject(RemoteObject owner) : this(owner.Connection)
    {
        Owner = owner;
    }

    public Task Pull()
    {
        lock (_pullLock) {
            if (PullSemaphore) {
                DiscardedPullCount++;
                return Task.CompletedTask;
            }
            PullSemaphore = true;
        }
        return Task.Run(async () => {
            await Connection.AwaitReady();
            OnPull();
            LastPullDate = DateTime.Now;
            PullSemaphore = false;
        });

    }

    public void RegisterPendingTask(Task task)
    {
        if (task.IsCompleted)
            return;

        PendingTaskCount += 1;

        Task.Run(async () => {
            await task;
            PendingTaskCount -= 1;
        });

        if (Owner != null) {
            Owner.RegisterPendingTask(task);
        }
    }


    protected abstract void OnPull();

    //protected abstract void OnUpdateData();

    public void BeginAutoPull(int interval)
    {

    }

    public void EndAutoPull()
    {


    }
}
