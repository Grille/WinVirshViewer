using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirshLib;

namespace WinVirshViewer;


public partial class RemoteListControl : UserControl
{
    private List<AllocatedBounds> _objLookupList;

    object _SelectedItem;
    public object SelectedItem
    {
        set
        {
            if (_SelectedItem != value)
            {
                _SelectedItem = value;
                OnSelectionChanged(EventArgs.Empty);
            }
        }
        get => _SelectedItem;
    }
    public object HoveredItem { private set; get; }

    public event EventHandler<EventArgs> SelectionChanged;

    public float ScrollPosition = 0;
    public int ItemScale = 20;

    public int VisibleItemCount => Height / ItemScale;

    protected float NextItemPosition;

    public RemoteListControl()
    {
        InitializeComponent();
        DoubleBuffered = true;
        _objLookupList = new List<AllocatedBounds>();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        UpdateHoveredItem(e);
    }

    protected void UpdateHoveredItem(MouseEventArgs e)
    {
        var old = HoveredItem;

        HoveredItem = null;
        foreach (var item in _objLookupList)
        {
            if (item.Rectangle.Contains(e.Location))
            {
                HoveredItem = item.Object;
            }
        }

        if (old != HoveredItem)
            Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (HoveredItem != null)
        {
            HoveredItem = null;
            Invalidate();
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        UpdateHoveredItem(e);

        SelectedItem = HoveredItem;
    }

    protected virtual void OnSelectionChanged(EventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
        Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);

        var old = ScrollPosition;

        if (e.Delta > 0)
            ScrollPosition += 2f;
        else
            ScrollPosition -= 2f;

        if (ScrollPosition > 0)
            ScrollPosition = 0;

        if (ScrollPosition < -NextItemPosition + 1)
            ScrollPosition = -NextItemPosition + 1;

        if (old != ScrollPosition)
        {
            VScrollBar.Value = Math.Clamp((int)(-ScrollPosition*10), VScrollBar.Minimum, VScrollBar.Minimum);

            OnMouseMove(e);
            Invalidate();
        }
    }

    protected void DrawPending(Graphics g, RemoteObject obj, Rectangle bounds)
    {
        if (obj.PendingTaskCount == 0)
            return;

        g.FillRectangle(new SolidBrush(Color.FromArgb(255 / 2, 255, 255, 255)), bounds);

        int centerX = bounds.X + bounds.Width / 2;
        int centerY = bounds.Y + bounds.Height / 2;

        float deg = DateTime.Now.Ticks / 100000 % 360;

        g.RotateTransform(deg, MatrixOrder.Append);
        g.TranslateTransform(centerX, centerY, MatrixOrder.Append);

        var normalBounds = new Rectangle(-bounds.Width / 2, -bounds.Height / 2, bounds.Width, bounds.Height);

        g.DrawImage(EmbeddedResources.Pending, normalBounds);

        g.ResetTransform();

        if (obj.PendingTaskCount > 1)
        {
            g.DrawString(obj.PendingTaskCount.ToString(), Font, Brushes.Black, bounds);
        }
    }

    protected record class AllocatedBounds(object Object, Rectangle Rectangle);

    protected void ClearItemList()
    {
        NextItemPosition = 0;
        _objLookupList.Clear();
    }

    protected Rectangle AllocItemBounds(object obj, float size)
    {
        float position = (NextItemPosition + ScrollPosition) * ItemScale;
        var rect = new Rectangle(0, (int)position, Width, (int)(ItemScale * size));

        NextItemPosition += size;

        _objLookupList.Add(new(obj, rect));

        return rect;
    }

    protected void AllocItemAtBounds(object obj, Rectangle bounds)
    {
        _objLookupList.Add(new(obj, bounds));
    }

    protected void UpdateVScrollBarMaximum()
    {
        VScrollBar.LargeChange = 200;
        VScrollBar.Maximum = (int)(NextItemPosition * 10 + VScrollBar.LargeChange - 10);
    }

    private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
        ScrollPosition = -(VScrollBar.Value/10f);
        Invalidate();
    }


}
