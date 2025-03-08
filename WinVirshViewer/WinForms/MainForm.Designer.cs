using System.Windows.Forms;

namespace WinVirshViewer
{

    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            addConnctionToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            hostToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripHost = new ContextMenuStrip(components);
            newVirtualMachineToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            editToolStripMenuItem = new ToolStripMenuItem();
            reconnectToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            vMToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripVm = new ContextMenuStrip(components);
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            startToolStripMenuItem = new ToolStripMenuItem();
            suspendToolStripMenuItem = new ToolStripMenuItem();
            shutdownOptionsToolStripMenuItem = new ToolStripMenuItem();
            shutdownToolStripMenuItem = new ToolStripMenuItem();
            rebootToolStripMenuItem = new ToolStripMenuItem();
            destroyToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            copyToolStripMenuItem = new ToolStripMenuItem();
            migrateToolStripMenuItem = new ToolStripMenuItem();
            takeSnapshotToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            imageList1 = new ImageList(components);
            vmListControl = new VmListControl();
            statusStrip1 = new StatusStrip();
            menuStrip1.SuspendLayout();
            contextMenuStripHost.SuspendLayout();
            contextMenuStripVm.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, hostToolStripMenuItem, vMToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(784, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addConnctionToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // addConnctionToolStripMenuItem
            // 
            addConnctionToolStripMenuItem.Image = Properties.Resources.New;
            addConnctionToolStripMenuItem.Name = "addConnctionToolStripMenuItem";
            addConnctionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            addConnctionToolStripMenuItem.Text = "Add Connction...";
            addConnctionToolStripMenuItem.Click += addConnctionToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Image = Properties.Resources.Exit;
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // hostToolStripMenuItem
            // 
            hostToolStripMenuItem.DropDown = contextMenuStripHost;
            hostToolStripMenuItem.Name = "hostToolStripMenuItem";
            hostToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            hostToolStripMenuItem.Text = "Host";
            // 
            // contextMenuStripHost
            // 
            contextMenuStripHost.Items.AddRange(new ToolStripItem[] { newVirtualMachineToolStripMenuItem, toolStripSeparator2, editToolStripMenuItem, reconnectToolStripMenuItem, removeToolStripMenuItem });
            contextMenuStripHost.Name = "contextMenuStripHost";
            contextMenuStripHost.Size = new System.Drawing.Size(185, 98);
            // 
            // newVirtualMachineToolStripMenuItem
            // 
            newVirtualMachineToolStripMenuItem.Image = Properties.Resources.New;
            newVirtualMachineToolStripMenuItem.Name = "newVirtualMachineToolStripMenuItem";
            newVirtualMachineToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            newVirtualMachineToolStripMenuItem.Text = "New Virtual Machine";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Image = Properties.Resources.Edit;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // reconnectToolStripMenuItem
            // 
            reconnectToolStripMenuItem.Image = Properties.Resources.Restart;
            reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            reconnectToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            reconnectToolStripMenuItem.Text = "Reconnect";
            reconnectToolStripMenuItem.Click += reconnectToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Image = Properties.Resources.Delete;
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // vMToolStripMenuItem
            // 
            vMToolStripMenuItem.DropDown = contextMenuStripVm;
            vMToolStripMenuItem.Name = "vMToolStripMenuItem";
            vMToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            vMToolStripMenuItem.Text = "VM";
            // 
            // contextMenuStripVm
            // 
            contextMenuStripVm.Items.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator3, startToolStripMenuItem, suspendToolStripMenuItem, shutdownOptionsToolStripMenuItem, toolStripSeparator4, copyToolStripMenuItem, migrateToolStripMenuItem, takeSnapshotToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuStripVm.Name = "contextMenuStripVm";
            contextMenuStripVm.Size = new System.Drawing.Size(150, 192);
            contextMenuStripVm.Opening += contextMenuStripVm_Opening;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Properties.Resources.Open;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(146, 6);
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Image = Properties.Resources.Run;
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.Click += startMenuItem_Click;
            // 
            // suspendToolStripMenuItem
            // 
            suspendToolStripMenuItem.Image = Properties.Resources.Pause;
            suspendToolStripMenuItem.Name = "suspendToolStripMenuItem";
            suspendToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            suspendToolStripMenuItem.Text = "Suspend";
            suspendToolStripMenuItem.Click += suspendMenuItem_Click;
            // 
            // shutdownOptionsToolStripMenuItem
            // 
            shutdownOptionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { shutdownToolStripMenuItem, rebootToolStripMenuItem, destroyToolStripMenuItem });
            shutdownOptionsToolStripMenuItem.Image = Properties.Resources.Stop;
            shutdownOptionsToolStripMenuItem.Name = "shutdownOptionsToolStripMenuItem";
            shutdownOptionsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            shutdownOptionsToolStripMenuItem.Text = "Shutdown";
            // 
            // shutdownToolStripMenuItem
            // 
            shutdownToolStripMenuItem.Image = Properties.Resources.ShutDown;
            shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
            shutdownToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            shutdownToolStripMenuItem.Text = "Shutdown";
            shutdownToolStripMenuItem.Click += shutdownMenuItem_Click;
            // 
            // rebootToolStripMenuItem
            // 
            rebootToolStripMenuItem.Image = Properties.Resources.Restart;
            rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            rebootToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            rebootToolStripMenuItem.Text = "Reboot";
            rebootToolStripMenuItem.Click += restartMenuItem_Click;
            // 
            // destroyToolStripMenuItem
            // 
            destroyToolStripMenuItem.Image = Properties.Resources.Stop;
            destroyToolStripMenuItem.Name = "destroyToolStripMenuItem";
            destroyToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            destroyToolStripMenuItem.Text = "Destroy";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(146, 6);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            copyToolStripMenuItem.Text = "Clone";
            // 
            // migrateToolStripMenuItem
            // 
            migrateToolStripMenuItem.Enabled = false;
            migrateToolStripMenuItem.Name = "migrateToolStripMenuItem";
            migrateToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            migrateToolStripMenuItem.Text = "Migrate";
            // 
            // takeSnapshotToolStripMenuItem
            // 
            takeSnapshotToolStripMenuItem.Image = Properties.Resources.New;
            takeSnapshotToolStripMenuItem.Name = "takeSnapshotToolStripMenuItem";
            takeSnapshotToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            takeSnapshotToolStripMenuItem.Text = "Take Snapshot";
            takeSnapshotToolStripMenuItem.Click += takeSnapshotToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new System.Drawing.Size(16, 16);
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // vmListControl
            // 
            vmListControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            vmListControl.BorderStyle = BorderStyle.Fixed3D;
            vmListControl.Location = new System.Drawing.Point(0, 24);
            vmListControl.Margin = new Padding(0);
            vmListControl.Name = "vmListControl";
            vmListControl.SelectedItem = null;
            vmListControl.Size = new System.Drawing.Size(784, 515);
            vmListControl.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.AutoSize = false;
            statusStrip1.Location = new System.Drawing.Point(0, 539);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(784, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(statusStrip1);
            Controls.Add(vmListControl);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "WinVirshViewer";
            FormClosing += MainForm_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStripHost.ResumeLayout(false);
            contextMenuStripVm.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem addConnctionToolStripMenuItem;
        private ImageList imageList1;
        private VmListControl vmListControl;
        private ToolStripMenuItem vMToolStripMenuItem;
        private ContextMenuStrip contextMenuStripVm;
        private ContextMenuStrip contextMenuStripHost;
        private ToolStripMenuItem suspendToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem shutdownOptionsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem hostToolStripMenuItem;
        private ToolStripMenuItem newVirtualMachineToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem reconnectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem shutdownToolStripMenuItem;
        private ToolStripMenuItem rebootToolStripMenuItem;
        private ToolStripMenuItem destroyToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem takeSnapshotToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem migrateToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
    }
}
