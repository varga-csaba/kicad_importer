namespace KiCadImport
{
    partial class Form1
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnRefreshKiCad = new Button();
            btnImport = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            rtbConsole = new RichTextBox();
            tbKicadExe = new TextBox();
            tbProjectFolder = new TextBox();
            tbDownloadsFolder = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnProjectFolder = new Button();
            btnDownloadsFolder = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Component = new GroupBox();
            btnScanFolder = new Button();
            cbRemove = new CheckBox();
            btnKicadExe = new Button();
            openFileDialog1 = new OpenFileDialog();
            menuStrip1.SuspendLayout();
            Component.SuspendLayout();
            SuspendLayout();
            // 
            // btnRefreshKiCad
            // 
            btnRefreshKiCad.Location = new Point(195, 114);
            btnRefreshKiCad.Name = "btnRefreshKiCad";
            btnRefreshKiCad.Size = new Size(96, 23);
            btnRefreshKiCad.TabIndex = 1;
            btnRefreshKiCad.Text = "Refresh KiCad";
            btnRefreshKiCad.UseVisualStyleBackColor = true;
            btnRefreshKiCad.Click += button2_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(114, 114);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 2;
            btnImport.Text = "Import";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(575, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // rtbConsole
            // 
            rtbConsole.Location = new Point(12, 335);
            rtbConsole.Name = "rtbConsole";
            rtbConsole.ReadOnly = true;
            rtbConsole.Size = new Size(551, 229);
            rtbConsole.TabIndex = 4;
            rtbConsole.Text = "";
            rtbConsole.TextChanged += rtbConsole_TextChanged;
            // 
            // tbKicadExe
            // 
            tbKicadExe.Location = new Point(44, 27);
            tbKicadExe.Name = "tbKicadExe";
            tbKicadExe.Size = new Size(413, 23);
            tbKicadExe.TabIndex = 5;
            // 
            // tbProjectFolder
            // 
            tbProjectFolder.Location = new Point(44, 56);
            tbProjectFolder.Name = "tbProjectFolder";
            tbProjectFolder.Size = new Size(413, 23);
            tbProjectFolder.TabIndex = 6;
            // 
            // tbDownloadsFolder
            // 
            tbDownloadsFolder.Location = new Point(44, 85);
            tbDownloadsFolder.Name = "tbDownloadsFolder";
            tbDownloadsFolder.Size = new Size(413, 23);
            tbDownloadsFolder.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(463, 30);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 10;
            label1.Text = "KiCad exe";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(463, 59);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 11;
            label2.Text = "Project Folder";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(463, 88);
            label3.Name = "label3";
            label3.Size = new Size(102, 15);
            label3.TabIndex = 12;
            label3.Text = "Downloads Folder";
            // 
            // btnProjectFolder
            // 
            btnProjectFolder.Image = Properties.Resources.open_folder;
            btnProjectFolder.Location = new Point(10, 56);
            btnProjectFolder.Name = "btnProjectFolder";
            btnProjectFolder.Size = new Size(26, 23);
            btnProjectFolder.TabIndex = 13;
            btnProjectFolder.UseVisualStyleBackColor = true;
            btnProjectFolder.Click += btnProjectFolder_Click;
            // 
            // btnDownloadsFolder
            // 
            btnDownloadsFolder.Image = Properties.Resources.open_folder;
            btnDownloadsFolder.Location = new Point(10, 85);
            btnDownloadsFolder.Name = "btnDownloadsFolder";
            btnDownloadsFolder.Size = new Size(26, 23);
            btnDownloadsFolder.TabIndex = 14;
            btnDownloadsFolder.UseVisualStyleBackColor = true;
            btnDownloadsFolder.Click += btnDownloadsFolder_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(6, 22);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(541, 158);
            flowLayoutPanel1.TabIndex = 15;
            // 
            // Component
            // 
            Component.Controls.Add(flowLayoutPanel1);
            Component.Location = new Point(10, 143);
            Component.Name = "Component";
            Component.Size = new Size(553, 186);
            Component.TabIndex = 16;
            Component.TabStop = false;
            Component.Text = "Components";
            // 
            // btnScanFolder
            // 
            btnScanFolder.Location = new Point(10, 114);
            btnScanFolder.Name = "btnScanFolder";
            btnScanFolder.Size = new Size(98, 23);
            btnScanFolder.TabIndex = 17;
            btnScanFolder.Text = "Scan folder";
            btnScanFolder.UseVisualStyleBackColor = true;
            btnScanFolder.Click += btnScanFolder_Click;
            // 
            // cbRemove
            // 
            cbRemove.AutoSize = true;
            cbRemove.Location = new Point(410, 116);
            cbRemove.Name = "cbRemove";
            cbRemove.Size = new Size(155, 19);
            cbRemove.TabIndex = 18;
            cbRemove.Text = "Remove imported folder";
            cbRemove.UseVisualStyleBackColor = true;
            // 
            // btnKicadExe
            // 
            btnKicadExe.Image = Properties.Resources.open_folder;
            btnKicadExe.Location = new Point(10, 27);
            btnKicadExe.Name = "btnKicadExe";
            btnKicadExe.Size = new Size(26, 23);
            btnKicadExe.TabIndex = 19;
            btnKicadExe.UseVisualStyleBackColor = true;
            btnKicadExe.Click += btnKicadExe_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 576);
            Controls.Add(btnKicadExe);
            Controls.Add(cbRemove);
            Controls.Add(btnScanFolder);
            Controls.Add(Component);
            Controls.Add(btnDownloadsFolder);
            Controls.Add(btnProjectFolder);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbDownloadsFolder);
            Controls.Add(tbProjectFolder);
            Controls.Add(tbKicadExe);
            Controls.Add(rtbConsole);
            Controls.Add(btnImport);
            Controls.Add(btnRefreshKiCad);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(591, 615);
            MinimumSize = new Size(591, 615);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Importer";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            Component.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnRefreshKiCad;
        private Button btnImport;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private RichTextBox rtbConsole;
        private TextBox tbKicadExe;
        private TextBox tbProjectFolder;
        private TextBox tbDownloadsFolder;
        private Label label1;
        private Label label2;
        private Label label3;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnProjectFolder;
        private Button btnDownloadsFolder;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox Component;
        private Button btnScanFolder;
        private CheckBox cbRemove;
        private Button btnKicadExe;
        private OpenFileDialog openFileDialog1;
    }
}
