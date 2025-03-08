using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirshLib;

public class LiveDataGraph
{
    public record class Entry(DateTime Date, float Value);
    public record class NormalizedEntry(float posX, float posY);

    private readonly object _lock = new object();

    private readonly List<Entry> entries = new List<Entry>();

    public TimeSpan ViewTimeSpan { get; set; } = TimeSpan.FromMinutes(1);
    public float MaxValue { get; set; } = 1;
    public float LastValue => entries[entries.Count - 1].Value;


    public LiveDataGraph()
    {
        Add(0);
    }

    public void Add(float value)
    {
        lock (_lock) {
            entries.Add(new Entry(DateTime.Now, value));
        }
    }

    public List<NormalizedEntry> GetNormalizedEntries()
    {
        var endDate = DateTime.Now;

        var startDate = endDate - ViewTimeSpan;

        var points = new List<NormalizedEntry>();

        lock (_lock) {
            for (var i = entries.Count - 1; i >= 0; i--) {
                var entry = entries[i];

                if (entry.Date < startDate)
                    return points;

                double totalDurationTicks = (endDate - startDate).Ticks;
                double progressDurationTicks = (entry.Date - startDate).Ticks;

                float progress = (float)(progressDurationTicks / totalDurationTicks);
                float value = entry.Value / MaxValue;

                points.Add(new(progress, value));
            }

        }

        return points;
    }
}
