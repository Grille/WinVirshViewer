using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinVirshViewer.WinForms;

[DesignerCategory("form")]
public class UpdateableForm : Form
{
    Task _updateTask = Task.CompletedTask;

    protected readonly Timer ContentUpdateTimer;
    protected readonly Timer RenderUpdateTimer;

    public UpdateableForm() { 
        ContentUpdateTimer = new Timer();
        RenderUpdateTimer = new Timer();

        ContentUpdateTimer.Interval = 1000;
        RenderUpdateTimer.Interval = 50;

        ContentUpdateTimer.Start();
        RenderUpdateTimer.Start();

        ContentUpdateTimer.Tick += _contentTimer_Tick;
        RenderUpdateTimer.Tick += _renderTimer_Tick;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        ContentUpdateTimer.Stop();
        RenderUpdateTimer.Stop();

        base.OnClosing(e);
    }

    private void _renderTimer_Tick(object sender, EventArgs e)
    {
        if (DesignMode)
            return;
        OnRender();
    }

    private void _contentTimer_Tick(object sender, EventArgs e)
    {
        if (DesignMode)
            return;
        UpdateContent();
    }

    public async void UpdateContent()
    {
        if (!_updateTask.IsCompleted)
            return;

        _updateTask = OnUpdateContent();

        await OnUpdateContent();
    }

    protected async virtual Task OnUpdateContent()
    {
        await Task.CompletedTask;
    }

    protected virtual void OnRender()
    {
        Invalidate();
    }

}
