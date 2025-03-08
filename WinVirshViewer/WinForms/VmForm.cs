using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VirshLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinVirshViewer.WinForms;

[DesignerCategory("form")]
public partial class VmForm : UpdateableForm
{
    public readonly VirtualMachine VirtualMachine;

    public VmForm(VirtualMachine vm)
    {
        InitializeComponent();

        DoubleBuffered = true;

        VirtualMachine = vm;
        snapshotListControl.VirtualMachine = vm;
        snapshotListControl.Font = new Font("consolas", snapshotListControl.ItemScale / 2);
        snapshotListControl.SnapshotContextMenu = contextMenuStripSnapshot;

        contextMenuStripSnapshot.Show();


        UpdateContent();
    }

    protected override void OnRender()
    {
        snapshotListControl.Invalidate();
    }

    protected override async Task OnUpdateContent()
    {
        var vm = VirtualMachine;

        await vm.Pull();
        await vm.SnapshotList.Pull();

        Text = $"{vm.Name} ({vm.State})";


        if (tabControl.SelectedTab == tabPageSnapshots) {
            snapshotListControl.Invalidate();
        }


        string xml = vm.XmlSource;
        if (textBox1.Text != xml)
            textBox1.Text = xml;

        startToolStripMenuItem.Enabled = vm.State != VmState.Running;
        startToolStripMenuItem.Text = vm.State == VmState.Paused ? "Resume" : "Start";
        suspendToolStripMenuItem.Enabled = vm.State == VmState.Running;
        shutdownToolStripMenuItem.Enabled = vm.State != VmState.ShutOff;
        rebootToolStripMenuItem.Enabled = vm.State == VmState.Running;

        toolStripStatusLabelUpdate.Text = $"last update {DateTime.Now}";
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateContent();
    }

    private void createToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var form = new CreateSnapshotForm(this, VirtualMachine);
        if (form.ShowDialog() == DialogResult.OK) {
            form.ApplyTo(VirtualMachine);
        }
    }

    private void revertToolStripMenuItem_Click(object sender, EventArgs e)
    {
        snapshotListControl.SelectedSnapshot.Revert().ShowOnError(this, "Revert Failed");
    }

    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        snapshotListControl.SelectedSnapshot.Delete().ShowOnError(this, "Delete Failed");
    }

    private void contextMenuStripSnapshot_Opening(object sender, CancelEventArgs e)
    {
        var snapshotActionsEnabled = snapshotListControl.SelectedItem != null && tabControl.SelectedTab == tabPageSnapshots;
        revertToolStripMenuItem.Enabled = snapshotActionsEnabled;
        deleteToolStripMenuItem.Enabled = snapshotActionsEnabled;
    }

    private void startToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (VirtualMachine.State == VmState.Paused) {
            VirtualMachine.Resume().ShowOnError(this, "Resume Failed");
        }
        else {
            VirtualMachine.Start().ShowOnError(this, "Start Failed");
        }
    }

    private void suspendToolStripMenuItem_Click(object sender, EventArgs e)
    {
        VirtualMachine.Suspend().ShowOnError(this, "Suspend Failed");
    }

    private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
    {
        VirtualMachine.Shutdown().ShowOnError(this, "Shutdown Failed");
    }

    private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
    {
        VirtualMachine.Reboot().ShowOnError(this, "Reboot Failed");
    }

    private void destroyToolStripMenuItem_Click(object sender, EventArgs e)
    {
        VirtualMachine.Destroy().ShowOnError(this, "Destroy Failed");
    }

    private void VmForm_Load(object sender, EventArgs e)
    {

    }
}
