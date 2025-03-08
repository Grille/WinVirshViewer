
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirshLib;
using WinVirshViewer.WinForms;

namespace WinVirshViewer;


[Editor()]
public partial class VmListControl : RemoteListControl
{
    public VirshHost SelectedHost { private set; get; }
    public VirtualMachine SelectedVm { private set; get; }

    public VirshHostList Hosts;

    public ContextMenuStrip ContextMenuVm;
    public ContextMenuStrip ContextMenuHost;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (Hosts == null)
            return;

        var g = e.Graphics;

        ClearItemList();
        foreach (var host in Hosts)
        {
            DrawHostItem(g, host);
        }

        UpdateVScrollBarMaximum();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (HoveredItem is ToggleButton btn) {
            if (e.Button == MouseButtons.Left) {
                btn.Target.Visible = !btn.Target.Visible;
                Invalidate();
            }
        }
        else if (SelectedItem is VirshHost) {
            if (e.Button == MouseButtons.Right) {
                ContextMenuHost.Show(this, e.Location);
            }
        }
        else if (SelectedItem is VirtualMachine) {
            if (e.Button == MouseButtons.Right) {
                ContextMenuVm.Show(this, e.Location);
            }
        }
    }

    protected override void OnSelectionChanged(EventArgs e)
    {
        if (SelectedItem is VirshHost host) {
            SelectedHost = host;
            SelectedVm = null;
        }
        else if (SelectedItem is VirtualMachine vm) {
            SelectedHost = vm.Owner;
            SelectedVm = vm;
        }
        else {
            SelectedHost = null;
            SelectedVm = null;
        }

        base.OnSelectionChanged(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
        base.OnMouseDoubleClick(e);

        if (e.Button != MouseButtons.Left)
            return;

        if (HoveredItem is VirshHost host) {
            using var form = new ConnectionSettingsForm(host);
            if (form.ShowDialog() == DialogResult.OK) {
                form.ApplyTo(host);
                Hosts.Save();
            }
        }
        else if (HoveredItem is VirtualMachine vm) {
            var form = new VmForm(vm);
            form.Show();
        }
    }

    record class ToggleButton(VirshHost Target);

    private void DrawHostItem(Graphics g, VirshHost host)
    {
        var bounds = AllocItemBounds(host, 1);

        var boundsButton = new Rectangle(bounds.X, bounds.Y, bounds.Height, bounds.Height);

        AllocItemAtBounds(new ToggleButton(host), boundsButton);

        bool btnHovered = HoveredItem is ToggleButton && ((ToggleButton)HoveredItem).Target == host;

        if (host == HoveredItem || btnHovered)
            g.FillRectangle(Brushes.LightGreen, bounds);
        if (host == SelectedItem)
            g.FillRectangle(Brushes.LimeGreen, bounds);

        if (btnHovered)
            g.FillRectangle(Brushes.Lime, boundsButton);

        string state = host.ConnectionState switch {
            ConnectState.Connecting => "Connecting...",
            ConnectState.AuthenticationFailed => "Authentication Failed",
            _ => host.ConnectionState.ToString(),
        };

        g.DrawString($"{(host.Visible ? "⮛" : "⮚")} Virsh: {host.FullAddress} - {state}", Font, Brushes.Black, bounds);

        if (!host.Visible)
            return;

        if (host.VirtualMachines.Error) {
            DrawMessageItem(g, "<Error>", Brushes.Red);
        }
        else if (host.VirtualMachines.Count == 0) {
            DrawMessageItem(g, "<Empty>", Brushes.Black);
        }
        else {
            foreach (var vm in host.VirtualMachines) {
                DrawVmItem(g, vm);
            }
        }

    }

    private void DrawVmItem(Graphics g, VirtualMachine vm)
    {
        var bounds = AllocItemBounds(vm, 2);

        var boundsImage = new Rectangle(bounds.Height / 2, bounds.Y, bounds.Height, bounds.Height);

        int boundsTextOffsetX = boundsImage.X + boundsImage.Width + bounds.Height / 4;
        var boundsText = new Rectangle(boundsTextOffsetX, bounds.Y, bounds.Width - boundsTextOffsetX, bounds.Height);

        if (vm == HoveredItem)
            g.FillRectangle(Brushes.LightGreen, bounds);
        if (vm == SelectedItem)
            g.FillRectangle(Brushes.LimeGreen, bounds);

        var img = vm.State switch
        {
            VmState.Running => EmbeddedResources.VmStateRunning,
            VmState.Paused => EmbeddedResources.VmStatePaused,
            VmState.ShutOff => EmbeddedResources.VmStateShutOff,
            _ => EmbeddedResources.VmStateError,
        };
        g.DrawImage(img, boundsImage);
        DrawPending(g, vm, boundsImage);

        //g.DrawGraph(vm.Stats.CpuUsage, new Rectangle(bounds.Height*6, bounds.Y, bounds.Height* 4, bounds.Height-1));
        //g.DrawGraph(vm.Stats.MemoryUsage, new Rectangle((int)(bounds.Height * 10f), bounds.Y, bounds.Height * 4, bounds.Height-1));

        //g.FillRectangle(Brushes.Blue, boundsImage);
        g.DrawString($"{vm.Name}\n{vm.State}", Font, Brushes.Black, boundsText);
    }

    private void DrawMessageItem(Graphics g, string msg, Brush brush)
    {
        var bounds = AllocItemBounds(null, 1);
        var boundsText = new Rectangle(bounds.Height, bounds.Y, bounds.Width - bounds.Height, bounds.Height);

        g.DrawString(msg, Font, brush, boundsText);
    }
}
