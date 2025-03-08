namespace WinVirshViewer
{
    partial class RemoteListControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            VScrollBar = new System.Windows.Forms.VScrollBar();
            SuspendLayout();
            // 
            // VScrollBar
            // 
            VScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            VScrollBar.Location = new System.Drawing.Point(383, 0);
            VScrollBar.Name = "VScrollBar";
            VScrollBar.Size = new System.Drawing.Size(17, 400);
            VScrollBar.TabIndex = 0;
            VScrollBar.Scroll += VScrollBar_Scroll;
            // 
            // RemoteListControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(VScrollBar);
            Name = "RemoteListControl";
            Size = new System.Drawing.Size(400, 400);
            ResumeLayout(false);
        }

        #endregion

        protected System.Windows.Forms.VScrollBar VScrollBar;
    }
}
