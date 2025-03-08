using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirshLib.VirtualMachine;
using System.Xml.Linq;

namespace VirshLib;


public class TaskQueue
{
    Task _executer;
    readonly Queue<Task> _queue;
    readonly object _lock = new object();

    public TaskQueue()
    {
        _queue = new Queue<Task>();
    }

    public void Enqueue(Task task)
    {
        lock (_queue) {
            _queue.Enqueue(task);
        }

        StartExecuter();
    }

    private void StartExecuter()
    {
        lock (_lock) {
            if (!(_executer == null || _executer.IsCompleted)) {
                return;
            }

            _executer = new Task(() => {
                //Console.WriteLine($"start {_queue.Count}");
                while (_queue.Count > 0) {
                    //Console.WriteLine($"@ {_queue.Count}");
                    Task task;
                    lock (_queue) {
                        task = _queue.Dequeue();
                    }
                    if (task.Status == TaskStatus.Created) {
                        task.Start();
                    }
                    task.Wait();
                }
                //Console.WriteLine("end");
            });
        }

        _executer.Start();
    }
}
