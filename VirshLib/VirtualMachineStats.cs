using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VirshLib;

public class VirtualMachineStats : RemoteObject
{
    public override VirtualMachine Owner => (VirtualMachine)base.Owner;

    public LiveDataGraph CpuUsage { get; }
    public LiveDataGraph DiskUsage { get; }
    public LiveDataGraph MemoryUsage { get; }

    public VirtualMachineStats(VirtualMachine owner) : base(owner)
    {
        CpuUsage = new LiveDataGraph();
        DiskUsage = new LiveDataGraph();
        MemoryUsage = new LiveDataGraph();
    }

    protected override void OnPull()
    {
        Console.WriteLine("pl");
        var domstatsProm = Connection.InfoShell.Command($"virsh domstats \"{Owner.Name}\"");
        var domstats = domstatsProm.Result;


        Console.WriteLine("exit");

        var map = new Dictionary<string, string>();

        
        foreach (var line in domstats) {
            var split = line.Split('=', 2);
            if (split.Length != 2)
                continue;

            string key = split[0].Trim().ToLower();
            string value = split[1].Trim();

            map.Add(key, value);

            Console.WriteLine(key + "=" + value);

            switch (key) {
                case "cpu.time":
                    CpuUsage.MaxValue = float.Parse(value);
                    break;
                case "cpu.system":
                    CpuUsage.Add(float.Parse(value));
                    //ID = int.Parse(value);
                    break;
                case "balloon.available":
                    MemoryUsage.MaxValue = float.Parse(value);
                    break;
                case "balloon.unused":
                    MemoryUsage.Add(float.Parse(value));
                    //ID = int.Parse(value);
                    break;
            }
        }
        
    }
}
