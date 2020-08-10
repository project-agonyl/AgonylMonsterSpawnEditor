namespace AgonylMonsterSpawnEditor
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CurrentMapFolderLabel = new System.Windows.Forms.Label();
            this.ReloadDataButton = new System.Windows.Forms.Button();
            this.ChooseMapFolderButton = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.MapDataLoaderBgWorker = new System.ComponentModel.BackgroundWorker();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CurrentMapFolderLabel);
            this.groupBox1.Controls.Add(this.ReloadDataButton);
            this.groupBox1.Controls.Add(this.ChooseMapFolderButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(713, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // CurrentMapFolderLabel
            // 
            this.CurrentMapFolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentMapFolderLabel.Location = new System.Drawing.Point(6, 16);
            this.CurrentMapFolderLabel.Name = "CurrentMapFolderLabel";
            this.CurrentMapFolderLabel.Size = new System.Drawing.Size(701, 23);
            this.CurrentMapFolderLabel.TabIndex = 5;
            this.CurrentMapFolderLabel.Text = "No Folder Chosen";
            this.CurrentMapFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReloadDataButton
            // 
            this.ReloadDataButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReloadDataButton.Location = new System.Drawing.Point(389, 50);
            this.ReloadDataButton.Name = "ReloadDataButton";
            this.ReloadDataButton.Size = new System.Drawing.Size(166, 23);
            this.ReloadDataButton.TabIndex = 3;
            this.ReloadDataButton.Text = "Reload Data";
            this.ReloadDataButton.UseVisualStyleBackColor = true;
            this.ReloadDataButton.Click += new System.EventHandler(this.ReloadDataButton_Click);
            // 
            // ChooseMapFolderButton
            // 
            this.ChooseMapFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseMapFolderButton.Location = new System.Drawing.Point(145, 50);
            this.ChooseMapFolderButton.Name = "ChooseMapFolderButton";
            this.ChooseMapFolderButton.Size = new System.Drawing.Size(166, 23);
            this.ChooseMapFolderButton.TabIndex = 2;
            this.ChooseMapFolderButton.Text = "Choose Map Folder";
            this.ChooseMapFolderButton.UseVisualStyleBackColor = true;
            this.ChooseMapFolderButton.Click += new System.EventHandler(this.ChooseMapFolderButton_Click);
            // 
            // MapDataLoaderBgWorker
            // 
            this.MapDataLoaderBgWorker.WorkerReportsProgress = true;
            this.MapDataLoaderBgWorker.WorkerSupportsCancellation = true;
            this.MapDataLoaderBgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MapDataLoaderBgWorker_DoWork);
            this.MapDataLoaderBgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MapDataLoaderBgWorker_ProgressChanged);
            this.MapDataLoaderBgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MapDataLoaderBgWorker_RunWorkerCompleted);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 110);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(713, 431);
            this.dataGridView.TabIndex = 8;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            this.dataGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragEnter);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 549);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agonyl Monster Spawn Editor";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label CurrentMapFolderLabel;
        private System.Windows.Forms.Button ReloadDataButton;
        private System.Windows.Forms.Button ChooseMapFolderButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.ComponentModel.BackgroundWorker MapDataLoaderBgWorker;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}

