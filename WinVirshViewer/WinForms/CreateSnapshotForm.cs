using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirshLib;
using WinVirshViewer.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WinVirshViewer;

public partial class CreateSnapshotForm : Form
{
    public new UpdateableForm Owner { get => (UpdateableForm)base.Owner; set => base.Owner = value; }

    public string SnapshotName => textBox1.Text;

    public CreateSnapshotForm(UpdateableForm owner, VirtualMachine vm)
    {
        Owner = owner;
        InitializeComponent();

        var snapshots = vm.SnapshotList;
        if (snapshots.DeltaSinceLastPull.TotalSeconds > 2) {
            snapshots.Pull().Wait();
        }
        Text = snapshots.Current == null ? "Create Snapshot" : $"Create Snapshot from '{snapshots.Current.Name}'";
    }

    public void ApplyTo(VirtualMachine vm)
    {
        vm.SnapshotList.Create(SnapshotName).ShowOnError(Owner, "Create Failed");
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
