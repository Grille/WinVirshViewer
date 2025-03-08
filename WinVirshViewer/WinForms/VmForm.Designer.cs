namespace WinVirshViewer.WinForms
{
    partial class VmForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            powerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            suspendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            shutdownOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            shutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            destroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            snapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuStripSnapshot = new System.Windows.Forms.ContextMenuStrip(components);
            createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            revertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabelUpdate = new System.Windows.Forms.ToolStripStatusLabel();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageConsole = new System.Windows.Forms.TabPage();
            label1 = new System.Windows.Forms.Label();
            tabPageDetails = new System.Windows.Forms.TabPage();
            label2 = new System.Windows.Forms.Label();
            tabPageXML = new System.Windows.Forms.TabPage();
            textBox1 = new System.Windows.Forms.TextBox();
            tabPageSnapshots = new System.Windows.Forms.TabPage();
            snapshotListControl = new SnapshotListControl();
            menuStrip1.SuspendLayout();
            contextMenuStripSnapshot.SuspendLayout();
            statusStrip.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageConsole.SuspendLayout();
            tabPageDetails.SuspendLayout();
            tabPageXML.SuspendLayout();
            tabPageSnapshots.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, powerToolStripMenuItem, snapshotToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(784, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Image = Properties.Resources.Exit;
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q;
            closeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // powerToolStripMenuItem
            // 
            powerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { startToolStripMenuItem, suspendToolStripMenuItem, shutdownOptionsToolStripMenuItem });
            powerToolStripMenuItem.Name = "powerToolStripMenuItem";
            powerToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            powerToolStripMenuItem.Text = "Power";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Image = Properties.Resources.Run;
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.Click += startToolStripMenuItem_Click;
            // 
            // suspendToolStripMenuItem
            // 
            suspendToolStripMenuItem.Image = Properties.Resources.Pause;
            suspendToolStripMenuItem.Name = "suspendToolStripMenuItem";
            suspendToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            suspendToolStripMenuItem.Text = "Suspend";
            suspendToolStripMenuItem.Click += suspendToolStripMenuItem_Click;
            // 
            // shutdownOptionsToolStripMenuItem
            // 
            shutdownOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { shutdownToolStripMenuItem, rebootToolStripMenuItem, destroyToolStripMenuItem });
            shutdownOptionsToolStripMenuItem.Image = Properties.Resources.Stop;
            shutdownOptionsToolStripMenuItem.Name = "shutdownOptionsToolStripMenuItem";
            shutdownOptionsToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            shutdownOptionsToolStripMenuItem.Text = "Shutdown";
            // 
            // shutdownToolStripMenuItem
            // 
            shutdownToolStripMenuItem.Image = Properties.Resources.ShutDown;
            shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
            shutdownToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            shutdownToolStripMenuItem.Text = "Shutdown";
            shutdownToolStripMenuItem.Click += shutdownToolStripMenuItem_Click;
            // 
            // rebootToolStripMenuItem
            // 
            rebootToolStripMenuItem.Image = Properties.Resources.Restart;
            rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            rebootToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            rebootToolStripMenuItem.Text = "Reboot";
            rebootToolStripMenuItem.Click += rebootToolStripMenuItem_Click;
            // 
            // destroyToolStripMenuItem
            // 
            destroyToolStripMenuItem.Image = Properties.Resources.Stop;
            destroyToolStripMenuItem.Name = "destroyToolStripMenuItem";
            destroyToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            destroyToolStripMenuItem.Text = "Destroy";
            destroyToolStripMenuItem.Click += destroyToolStripMenuItem_Click;
            // 
            // snapshotToolStripMenuItem
            // 
            snapshotToolStripMenuItem.DropDown = contextMenuStripSnapshot;
            snapshotToolStripMenuItem.Name = "snapshotToolStripMenuItem";
            snapshotToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            snapshotToolStripMenuItem.Text = "Snapshot";
            // 
            // contextMenuStripSnapshot
            // 
            contextMenuStripSnapshot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createToolStripMenuItem, toolStripSeparator1, revertToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuStripSnapshot.Name = "contextMenuStripSnapshot";
            contextMenuStripSnapshot.OwnerItem = snapshotToolStripMenuItem;
            contextMenuStripSnapshot.Size = new System.Drawing.Size(109, 76);
            contextMenuStripSnapshot.Opening += contextMenuStripSnapshot_Opening;
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.Image = Properties.Resources.New;
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            createToolStripMenuItem.Text = "Create";
            createToolStripMenuItem.Click += createToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(105, 6);
            // 
            // revertToolStripMenuItem
            // 
            revertToolStripMenuItem.Image = Properties.Resources.Open;
            revertToolStripMenuItem.Name = "revertToolStripMenuItem";
            revertToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            revertToolStripMenuItem.Text = "Revert";
            revertToolStripMenuItem.Click += revertToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Image = Properties.Resources.Delete;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabelUpdate });
            statusStrip.Location = new System.Drawing.Point(0, 539);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(784, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabelUpdate
            // 
            toolStripStatusLabelUpdate.Name = "toolStripStatusLabelUpdate";
            toolStripStatusLabelUpdate.Size = new System.Drawing.Size(69, 17);
            toolStripStatusLabelUpdate.Text = "Last Update";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageConsole);
            tabControl.Controls.Add(tabPageDetails);
            tabControl.Controls.Add(tabPageXML);
            tabControl.Controls.Add(tabPageSnapshots);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 24);
            tabControl.Margin = new System.Windows.Forms.Padding(0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(784, 515);
            tabControl.TabIndex = 2;
            tabControl.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageConsole
            // 
            tabPageConsole.Controls.Add(label1);
            tabPageConsole.Location = new System.Drawing.Point(4, 24);
            tabPageConsole.Name = "tabPageConsole";
            tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
            tabPageConsole.Size = new System.Drawing.Size(776, 487);
            tabPageConsole.TabIndex = 0;
            tabPageConsole.Text = "Console";
            tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            label1.Location = new System.Drawing.Point(3, 3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(770, 481);
            label1.TabIndex = 2;
            label1.Text = "WIP";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageDetails
            // 
            tabPageDetails.Controls.Add(label2);
            tabPageDetails.Location = new System.Drawing.Point(4, 24);
            tabPageDetails.Name = "tabPageDetails";
            tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            tabPageDetails.Size = new System.Drawing.Size(792, 376);
            tabPageDetails.TabIndex = 1;
            tabPageDetails.Text = "Details";
            tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            label2.Location = new System.Drawing.Point(3, 3);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(786, 370);
            label2.TabIndex = 1;
            label2.Text = "WIP";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageXML
            // 
            tabPageXML.Controls.Add(textBox1);
            tabPageXML.Location = new System.Drawing.Point(4, 24);
            tabPageXML.Margin = new System.Windows.Forms.Padding(0);
            tabPageXML.Name = "tabPageXML";
            tabPageXML.Size = new System.Drawing.Size(792, 376);
            tabPageXML.TabIndex = 2;
            tabPageXML.Text = "XML";
            tabPageXML.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox1.Location = new System.Drawing.Point(0, 0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBox1.Size = new System.Drawing.Size(792, 376);
            textBox1.TabIndex = 0;
            textBox1.WordWrap = false;
            // 
            // tabPageSnapshots
            // 
            tabPageSnapshots.Controls.Add(snapshotListControl);
            tabPageSnapshots.Location = new System.Drawing.Point(4, 24);
            tabPageSnapshots.Margin = new System.Windows.Forms.Padding(0);
            tabPageSnapshots.Name = "tabPageSnapshots";
            tabPageSnapshots.Size = new System.Drawing.Size(792, 376);
            tabPageSnapshots.TabIndex = 3;
            tabPageSnapshots.Text = "Snapshots";
            tabPageSnapshots.UseVisualStyleBackColor = true;
            // 
            // snapshotListControl
            // 
            snapshotListControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            snapshotListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            snapshotListControl.Location = new System.Drawing.Point(0, 0);
            snapshotListControl.Margin = new System.Windows.Forms.Padding(0);
            snapshotListControl.Name = "snapshotListControl";
            snapshotListControl.SelectedItem = null;
            snapshotListControl.Size = new System.Drawing.Size(792, 376);
            snapshotListControl.TabIndex = 1;
            // 
            // VmForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(tabControl);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "VmForm";
            Text = "VmForm";
            Load += VmForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStripSnapshot.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPageConsole.ResumeLayout(false);
            tabPageDetails.ResumeLayout(false);
            tabPageXML.ResumeLayout(false);
            tabPageXML.PerformLayout();
            tabPageSnapshots.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.TabPage tabPageXML;
        private System.Windows.Forms.TabPage tabPageSnapshots;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapshotToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSnapshot;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem powerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shutdownOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shutdownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem destroyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUpdate;
        private SnapshotListControl snapshotListControl;
        private System.Windows.Forms.Label label1;
    }
}