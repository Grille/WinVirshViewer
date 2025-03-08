
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirshLib;
using WinVirshViewer.WinForms;

namespace WinVirshViewer;


public partial class SnapshotListControl : RemoteListControl
{
    public Snapshot SelectedSnapshot { private set; get; }
    public VirtualMachine VirtualMachine;
    public ContextMenuStrip SnapshotContextMenu;


    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (e.Button == MouseButtons.Right) {
            SnapshotContextMenu.Show(this, e.Location);
        }
    }


    protected override void OnSelectionChanged(EventArgs e)
    {
        base.OnSelectionChanged(e);

        if (SelectedItem is Snapshot s) {
            SelectedSnapshot = s;
        }
        else {
            SelectedSnapshot = null;
        }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
        base.OnDoubleClick(e);

        if (HoveredItem is null) {
            return;
        }
        else if (HoveredItem is Snapshot snapshot) {

        }
        else {
            throw new Exception();
        }
    }



    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (VirtualMachine == null || VirtualMachine.SnapshotList == null)
            return;

        var g = e.Graphics;

        ClearItemList();
        foreach (var snapshot in VirtualMachine.SnapshotList.Root) {
            DrawSnapshotItem(g, snapshot, 0);
        }

        UpdateVScrollBarMaximum();
    }

    private void DrawSnapshotItem(Graphics g, Snapshot snapshot, int indentation)
    {
        var bounds = AllocItemBounds(snapshot, 2);

        var boundsImage = new Rectangle((bounds.Height / 2) * indentation, bounds.Y, bounds.Height, bounds.Height);


        int boundsTextOffsetX = boundsImage.X + boundsImage.Width + bounds.Height / 4;
        var boundsText = new Rectangle(boundsTextOffsetX, bounds.Y, bounds.Width - boundsTextOffsetX, bounds.Height);

        if (snapshot == HoveredItem)
            g.FillRectangle(Brushes.LightGreen, bounds);
        if (snapshot == SelectedItem)
            g.FillRectangle(Brushes.LimeGreen, bounds);

        if (snapshot == VirtualMachine.SnapshotList.Current) {
            g.FillRectangle(Brushes.Lime, boundsImage);
        }

        var img = snapshot.State switch {
            VmState.Running => EmbeddedResources.VmStateRunning,
            VmState.Paused => EmbeddedResources.VmStatePaused,
            VmState.ShutOff => EmbeddedResources.VmStateShutOff,
            _ => EmbeddedResources.VmStateError,
        };
        g.DrawImage(img, boundsImage);
        DrawPending(g, snapshot, boundsImage);

        //g.FillRectangle(Brushes.Blue, boundsImage);
        g.DrawString($"{snapshot.Name} {snapshot.CreationTime}\n{snapshot.State}", Font, Brushes.Black, boundsText);

        foreach (var child in snapshot.Children) {
            DrawSnapshotItem(g, child, indentation + 1);
        }
    }
}
