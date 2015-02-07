namespace UnbindSourceCodeFromTFS
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnUnbind = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dgvActions = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ActionColumn = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.ResultColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.DetailsColumns = new System.Windows.Forms.DataGridViewLinkColumn();
            this.chkBackup = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnOpenFolderDialog = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtProjectPath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblProjectPath = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.ProfessionalSystem;
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnUnbind);
            this.kryptonPanel.Controls.Add(this.dgvActions);
            this.kryptonPanel.Controls.Add(this.chkBackup);
            this.kryptonPanel.Controls.Add(this.btnOpenFolderDialog);
            this.kryptonPanel.Controls.Add(this.txtProjectPath);
            this.kryptonPanel.Controls.Add(this.lblProjectPath);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(606, 227);
            this.kryptonPanel.TabIndex = 0;
            // 
            // btnUnbind
            // 
            this.btnUnbind.Location = new System.Drawing.Point(504, 190);
            this.btnUnbind.Name = "btnUnbind";
            this.btnUnbind.TabIndex = 5;
            this.btnUnbind.Values.Text = "Unbind";
            this.btnUnbind.Click += new System.EventHandler(this.btnUnbind_Click);
            // 
            // dgvActions
            // 
            this.dgvActions.AllowUserToAddRows = false;
            this.dgvActions.AllowUserToDeleteRows = false;
            this.dgvActions.AllowUserToResizeColumns = false;
            this.dgvActions.AllowUserToResizeRows = false;
            this.dgvActions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ActionColumn,
            this.ResultColumn,
            this.DetailsColumns});
            this.dgvActions.Location = new System.Drawing.Point(13, 42);
            this.dgvActions.MultiSelect = false;
            this.dgvActions.Name = "dgvActions";
            this.dgvActions.ReadOnly = true;
            this.dgvActions.RowHeadersVisible = false;
            this.dgvActions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActions.Size = new System.Drawing.Size(581, 136);
            this.dgvActions.TabIndex = 4;
            this.dgvActions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvActions_CellClick);
            // 
            // ActionColumn
            // 
            this.ActionColumn.DataPropertyName = "Action";
            this.ActionColumn.HeaderText = "Action";
            this.ActionColumn.Name = "ActionColumn";
            this.ActionColumn.ReadOnly = true;
            this.ActionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ActionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ActionColumn.Width = 480;
            // 
            // ResultColumn
            // 
            this.ResultColumn.DataPropertyName = "Result";
            this.ResultColumn.HeaderText = "Result";
            this.ResultColumn.Name = "ResultColumn";
            this.ResultColumn.ReadOnly = true;
            this.ResultColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ResultColumn.Width = 50;
            // 
            // DetailsColumns
            // 
            this.DetailsColumns.ActiveLinkColor = System.Drawing.Color.SteelBlue;
            this.DetailsColumns.DataPropertyName = "Details";
            this.DetailsColumns.HeaderText = "";
            this.DetailsColumns.LinkColor = System.Drawing.Color.SteelBlue;
            this.DetailsColumns.Name = "DetailsColumns";
            this.DetailsColumns.ReadOnly = true;
            this.DetailsColumns.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DetailsColumns.TrackVisitedState = false;
            this.DetailsColumns.Width = 50;
            // 
            // chkBackup
            // 
            this.chkBackup.Checked = true;
            this.chkBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackup.Location = new System.Drawing.Point(492, 12);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(102, 22);
            this.chkBackup.TabIndex = 3;
            this.chkBackup.Values.Text = "Create Backup";
            // 
            // btnOpenFolderDialog
            // 
            this.btnOpenFolderDialog.Location = new System.Drawing.Point(462, 10);
            this.btnOpenFolderDialog.Name = "btnOpenFolderDialog";
            this.btnOpenFolderDialog.Size = new System.Drawing.Size(24, 25);
            this.btnOpenFolderDialog.TabIndex = 2;
            this.btnOpenFolderDialog.Values.Text = "...";
            this.btnOpenFolderDialog.Click += new System.EventHandler(this.btnOpenFolderDialog_Click);
            // 
            // txtProjectPath
            // 
            this.txtProjectPath.Location = new System.Drawing.Point(97, 12);
            this.txtProjectPath.Name = "txtProjectPath";
            this.txtProjectPath.Size = new System.Drawing.Size(359, 20);
            this.txtProjectPath.TabIndex = 1;
            // 
            // lblProjectPath
            // 
            this.lblProjectPath.Location = new System.Drawing.Point(12, 12);
            this.lblProjectPath.Name = "lblProjectPath";
            this.lblProjectPath.Size = new System.Drawing.Size(79, 22);
            this.lblProjectPath.TabIndex = 0;
            this.lblProjectPath.Values.Text = "Project Path:";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 227);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Unbind Source Code From TFS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;

        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkBackup;

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOpenFolderDialog;

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtProjectPath;

        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblProjectPath;

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvActions;

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUnbind;

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ActionColumn;

        private System.Windows.Forms.DataGridViewImageColumn ResultColumn;

        private System.Windows.Forms.DataGridViewLinkColumn DetailsColumns;

        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

