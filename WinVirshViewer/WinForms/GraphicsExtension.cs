using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirshLib;

namespace WinVirshViewer.WinForms;

public static class GraphicsExtension
{
    public static void DrawGraph(this Graphics g, LiveDataGraph graph, Rectangle rectangle)
    {
        var entries = graph.GetNormalizedEntries();

        /*
        var entries = new List<LiveDataGraph.NormalizedEntry>() {
            new(0.9f, 0.8f),
            new(0.5f, 0.2f),
            new(0.25f, 0.8f),
            new(0.1f, 0.2f),
        };
        */

        g.FillRectangle(Brushes.Lavender, rectangle);

        if (entries.Count > 0) {

            var polygon = new Point[entries.Count + 4];

            for (var i = 0; i < entries.Count; i++) {
                var entry = entries[i];
                polygon[i + 2] = new Point(
                    (int)(entry.posX * rectangle.Width + rectangle.X),
                    (int)((1 - entry.posY) * rectangle.Height + rectangle.Y)
                );
            }

            int x0 = rectangle.X + rectangle.Width;
            int x1 = rectangle.X;

            polygon[0] = new Point(x0, rectangle.Y + rectangle.Height);
            polygon[1] = new Point(x0, polygon[2].Y);

            polygon[polygon.Length - 2] = new Point(x1, polygon[polygon.Length - 3].Y);
            polygon[polygon.Length - 1] = new Point(x1, rectangle.Y + rectangle.Height);

            g.FillPolygon(Brushes.LimeGreen, polygon);
            g.DrawPolygon(Pens.DarkGreen, polygon);
        }

        /*
        var font = new Font("consolas", 10);
        g.DrawString($"{graph.MaxValue}", font, Brushes.Black, rectangle,new StringFormat());
        g.DrawString($"{graph.LastValue}", font, Brushes.Black, rectangle, new StringFormat() {Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });
        */

        g.DrawRectangle(Pens.DarkGreen, rectangle);
    }
}
