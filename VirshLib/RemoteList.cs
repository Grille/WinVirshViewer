using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VirshLib;

public abstract class RemoteList<TItem> : RemoteObject, IReadOnlyCollection<TItem> where TItem : RemoteObject, IHasName
{
    private List<TItem> _items;

    public RemoteList(RemoteObject owner) : base(owner)
    {
        _items = new List<TItem>();
    }

    protected void SetItems(List<TItem> items)
    {
        _items = items;
    }

    public TItem this[string name] {
        get {
            var item = GetByName(name);
            if (item == null)
                throw new KeyNotFoundException(name);
            return item;
        }
    }

    public TItem? GetByName(string name)
    {
        foreach (var item in this) {
            if (item.Name == name)
                return item;
        }
        return null;
    }

    public bool Exists(string name)
    {
        return GetByName(name) != null;
    }

    public void RegisterPendingTask(Task cmd, string name)
    {
        Task.Run(async () => {
            var beginDate = DateTime.Now;
            while (true) {
                var snapshot = GetByName(name);
                if (snapshot != null) {
                    snapshot.RegisterPendingTask(cmd);
                    return;
                }
                if (DateTime.Now - beginDate > TimeSpan.FromSeconds(10))
                    return;
                await Task.Delay(100);
            }
        });
    }

    public int Count => _items.Count;

    public IEnumerator<TItem> GetEnumerator()
    {
        return ((IEnumerable<TItem>)_items).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_items).GetEnumerator();
    }
}
