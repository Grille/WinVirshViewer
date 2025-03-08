using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirshLib;
using WinVirshViewer.WinForms;

namespace WinVirshViewer;

[DesignerCategory("form")]
public partial class MainForm : UpdateableForm
{
    VirshHostList hosts;

    public MainForm()
    {
        InitializeComponent();

        hosts = new VirshHostList("hosts.dat");

        DoubleBuffered = true;

        vmListControl.Hosts = hosts;

        vmListControl.Font = new System.Drawing.Font("consolas", vmListControl.ItemScale / 2);
        vmListControl.ContextMenuVm = contextMenuStripVm;
        vmListControl.ContextMenuHost = contextMenuStripHost;

        contextMenuStripVm.Show();
        contextMenuStripHost.Show();

        vmListControl.SelectionChanged += VmListControl1_SelectionChanged;


        VmListControl1_SelectionChanged(this, EventArgs.Empty);


        //var host0 = new VirshHost("paul", "172.16.0.20");
        //hosts.Add(host0);

        //host0.StartRefresh(1000);

        //vmTreeCtrl = new VmTreeCtrl(treeView1, hosts);

        //vmTreeCtrl.Update();

        UpdateContent();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (hosts.Count == 0) {
            addConnctionToolStripMenuItem_Click(null, EventArgs.Empty);
        }
    }

    protected override void OnRender()
    {
        vmListControl.Invalidate();
    }

    private void VmListControl1_SelectionChanged(object sender, EventArgs e)
    {
        vMToolStripMenuItem.Enabled = vmListControl.SelectedVm != null;
        hostToolStripMenuItem.Enabled = vmListControl.SelectedHost != null;
    }

    private void addConnctionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new ConnectionSettingsForm();

        if (dialog.ShowDialog() == DialogResult.OK) {
            var host = new VirshHost(dialog.User, dialog.Address, dialog.Password);
            hosts.Add(host);
            hosts.Save();
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {

    }

    private void startMenuItem_Click(object sender, EventArgs e)
    {
        if (vmListControl.SelectedVm.State == VmState.Paused) {
            vmListControl.SelectedVm.Resume().ShowOnError(this, "Resume Failed");
        }
        else {
            vmListControl.SelectedVm.Start().ShowOnError(this, "Start Failed");
        }
    }

    private void suspendMenuItem_Click(object sender, EventArgs e)
    {
        vmListControl.SelectedVm.Suspend().ShowOnError(this, "Suspend Failed");
    }

    private void shutdownMenuItem_Click(object sender, EventArgs e)
    {
        vmListControl.SelectedVm.Shutdown().ShowOnError(this, "Shutdown Failed");
    }

    private void restartMenuItem_Click(object sender, EventArgs e)
    {
        vmListControl.SelectedVm.Reboot().ShowOnError(this, "Reboot Failed");
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        UpdateContent();
    }

    protected override async Task OnUpdateContent()
    {
        foreach (var host in hosts) {
            if (host.Connection.IsConnected) {
                await host.Pull();
                await host.VirtualMachines.Pull();
            }
        }
        vmListControl.Invalidate();
    }

    private void contextMenuStripVm_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var menu = contextMenuStripVm;
        var vm = vmListControl.SelectedVm;

        if (vm == null) {
            menu.Enabled = false;
            return;
        }
        else {
            menu.Enabled = true;
        }

        startToolStripMenuItem.Enabled = vm.State != VmState.Running;
        startToolStripMenuItem.Text = vm.State == VmState.Paused ? "Resume" : "Start";
        suspendToolStripMenuItem.Enabled = vm.State == VmState.Running;
        shutdownOptionsToolStripMenuItem.Enabled = vm.State != VmState.ShutOff;
        rebootToolStripMenuItem.Enabled = vm.State == VmState.Running;

    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dialog = new ConnectionSettingsForm(vmListControl.SelectedHost);
        if (dialog.ShowDialog() == DialogResult.OK) {
            dialog.ApplyTo(vmListControl.SelectedHost);
            hosts.Save();
        }
    }

    private void takeSnapshotToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new CreateSnapshotForm(this, vmListControl.SelectedVm);
        if (form.ShowDialog() == DialogResult.OK) {
            form.ApplyTo(vmListControl.SelectedVm);
        }
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var form = new VmForm(vmListControl.SelectedVm);
        form.Show();
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var host = vmListControl.SelectedHost;
        hosts.Remove(host);
        hosts.Save();
    }

    private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var host = vmListControl.SelectedHost;
        host.RefreshConnection();
    }

    private void vMToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }
}
