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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WinVirshViewer;

public partial class ConnectionSettingsForm : Form
{
    public string User {
        get {
            var split = textBox1.Text.Split('@');
            return split[0];
        }
    }

    public string Address {
        get {
            var split = textBox1.Text.Split('@');
            if (split.Length > 1)
                return split[1];
            else
                return "";
        }
    }

    public string Password => textBox2.Text;

    public bool SavePassword => checkBox1.Checked;

    public ConnectionSettingsForm()
    {
        InitializeComponent();
    }

    public ConnectionSettingsForm(VirshHost host)
    {
        InitializeComponent();

        textBox1.Text = $"{host.User}@{host.Address}";
        textBox2.Text = host.Password;
        checkBox1.Checked = host.SavePassword;
    }

    public void ApplyTo(VirshHost host)
    {
        host.User = User;
        host.Address = Address;
        host.Password = Password;
        host.SavePassword = SavePassword;
        host.RefreshConnection();
        if (host.Connection.IsConnected) {
            host.Pull();
            host.VirtualMachines.Pull();
        }
    }

    private void tabPage1_Click(object sender, EventArgs e)
    {

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
