using Lumos.Windows.Api;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UnbindSourceCodeFromTFS.Properties;

namespace UnbindSourceCodeFromTFS
{
    public partial class MainForm : Form
    {
        private DataTable dataTable;

        private string projectPath;

        private bool createBackup;

        private int IMAGE_COL_INDEX = 1;

        private int LINK_COL_INDEX = 2;

        private bool currentlyAnimating = false;

        private bool cancelCloseForm = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                foreach (DataGridViewRow row in (IEnumerable)dgvActions.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        Image value = row.Cells[IMAGE_COL_INDEX].Value as Image;
                        if ((value == null ? false : ImageAnimator.CanAnimate(value)))
                        {
                            ImageAnimator.Animate(value, new EventHandler(OnFrameChanged));
                        }
                    }
                }
                
                currentlyAnimating = true;
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            UnbindSourceCode((UnbindInfo)e.Argument, backgroundWorker, e);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressInfo userState = (ProgressInfo)e.UserState;
            StopAnimateImage();
            if (!(userState.Step != 1 || userState.ProgressType != ProgressType.Finished ? true : createBackup))
            {
                dataTable.Rows[0]["Result"] = Resources.Minus;
            }
            else if (userState.ProgressType == ProgressType.Processing)
            {
                Image progress = Resources.Progress;
                dataTable.Rows[userState.Step - 1]["Result"] = progress;
                AnimateImage();
            }
            else if (!(userState.ProgressType != ProgressType.Finished ? true : userState.Error))
            {
                dataTable.Rows[userState.Step - 1]["Result"] = Resources.Correct;
            }
            else if ((userState.ProgressType != ProgressType.Finished ? false : userState.Error))
            {
                dataTable.Rows[userState.Step - 1]["Result"] = Resources.Wrong;
                dataTable.Rows[userState.Step - 1]["Details"] = "Details";
                dataTable.Rows[userState.Step - 1]["ErrorMessage"] = userState.ErrorMessage;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StopAnimateImage();
            EnabledControls(true);
            TaskbarManager.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        private void btnOpenFolderDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "Select the project path:",
                ShowNewFolderButton = false
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtProjectPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnUnbind_Click(object sender, EventArgs e)
        {
            UnbindSourceCode();
        }

        private void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            string[] files = Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < (int)files.Length; i++)
            {
                string str = files[i];
                string str1 = string.Concat(targetDirectory, str.Substring(sourceDirectory.Length));
                string directoryName = Path.GetDirectoryName(str1);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.Copy(str, str1, true);
            }
        }

        private void dgvActions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex != LINK_COL_INDEX || e.RowIndex < 0 ? false : !string.IsNullOrEmpty(dgvActions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())))
            {
                MessageBoxEx.Show(dataTable.Rows[e.RowIndex]["ErrorMessage"].ToString(), "Details", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void dgvActions_Paint(object sender, PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames();
        }

        private void EnabledControls(bool enabled)
        {
            if (enabled)
            {
                TaskbarManager.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            else
            {
                TaskbarManager.SetProgressState(TaskbarProgressBarState.Indeterminate);
            }

            txtProjectPath.Enabled = enabled;
            btnOpenFolderDialog.Enabled = enabled;
            chkBackup.Enabled = enabled;
            btnUnbind.Enabled = enabled;
            cancelCloseForm = !enabled;
        }

        private void InitializeDataGridView()
        {
            dgvActions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvActions.AutoGenerateColumns = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelCloseForm;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ResetDataSource();
            dgvActions.Paint += new PaintEventHandler(dgvActions_Paint);
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            dgvActions.Invalidate();
        }

        private void ResetDataSource()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Action");
            dataTable.Columns.Add("Result", typeof(Image));
            dataTable.Columns.Add("Details");
            dataTable.Columns.Add("ErrorMessage");
            DataRow empty = dataTable.NewRow();
            empty["Action"] = "Creating backup";
            empty["Result"] = Resources.Empty;
            empty["Details"] = "";
            empty["ErrorMessage"] = "";
            dataTable.Rows.Add(empty);
            empty = dataTable.NewRow();
            empty["Action"] = "Removing the read only attribute of all folders";
            empty["Result"] = Resources.Empty;
            empty["Details"] = "";
            empty["ErrorMessage"] = "";
            dataTable.Rows.Add(empty);
            empty = dataTable.NewRow();
            empty["Action"] = "Searching for *.scc , *.vssscc, *.vspscc to delete them";
            empty["Result"] = Resources.Empty;
            empty["Details"] = "";
            empty["ErrorMessage"] = "";
            dataTable.Rows.Add(empty);
            empty = dataTable.NewRow();
            empty["Action"] = "Deleting section GlobalSection(TeamFoundationVersionControl) from solution files";
            empty["Result"] = Resources.Empty;
            empty["Details"] = "";
            empty["ErrorMessage"] = "";
            dataTable.Rows.Add(empty);
            empty = dataTable.NewRow();
            empty["Action"] = "Deleting SccProjectName, SccLocalPath, SccAuxPath, SccProvider from project files";
            empty["Result"] = Resources.Empty;
            empty["Details"] = "";
            empty["ErrorMessage"] = "";
            dataTable.Rows.Add(empty);
            dgvActions.DataSource = dataTable;
        }

        private void StopAnimateImage()
        {
            if (currentlyAnimating)
            {
                foreach (DataGridViewRow row in (IEnumerable)dgvActions.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        Image value = row.Cells[IMAGE_COL_INDEX].Value as Image;
                        if ((value == null ? false : ImageAnimator.CanAnimate(value)))
                        {
                            ImageAnimator.StopAnimate(value, new EventHandler(OnFrameChanged));
                        }
                    }
                }

                currentlyAnimating = false;
            }
        }

        private void UnbindSourceCode()
        {
            projectPath = txtProjectPath.Text;
            createBackup = chkBackup.Checked;
            ResetDataSource();
            EnabledControls(false);
            UnbindInfo unbindInfo = new UnbindInfo(projectPath, createBackup);
            backgroundWorker.RunWorkerAsync(unbindInfo);
        }

        private void UnbindSourceCode(UnbindInfo unbindInfo, BackgroundWorker worker, DoWorkEventArgs e)
        {
            string content;
            string section;
            string[] files;
            string directory;
            Exception exception;
            string[] directories;
            int i;
            bool flag = false;
            string message = "";
            DateTime date = DateTime.Now.Date;
            string backupPath = Path.Combine(projectPath, string.Format("Backup-{0}", date.ToString("yyyy-MM-dd")));
            
            if (unbindInfo.CreateBackup)
            {
                worker.ReportProgress(0, new ProgressInfo(1, ProgressType.Processing));
                try
                {
                    if (Directory.Exists(backupPath))
                    {
                        Directory.Delete(backupPath, true);
                    }
                    Directory.CreateDirectory(backupPath);
                    directories = Directory.GetDirectories(projectPath);
                    for (i = 0; i < (int)directories.Length; i++)
                    {
                        string str5 = directories[i];
                        if (!string.Equals(str5, backupPath, StringComparison.InvariantCultureIgnoreCase))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(str5);
                            string str6 = Path.Combine(backupPath, directoryInfo.Name);
                            CopyDirectory(str5, str6);
                        }
                    }
                    directories = Directory.GetFiles(projectPath);
                    for (i = 0; i < (int)directories.Length; i++)
                    {
                        directory = directories[i];
                        string str7 = string.Concat(backupPath, directory.Substring(projectPath.Length));
                        File.Copy(directory, str7, true);
                    }
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    flag = true;
                    message = exception.Message;
                }
            }
            if (flag)
            {
                worker.ReportProgress(20, new ProgressInfo(1, ProgressType.Finished, true, message));
            }
            else
            {
                worker.ReportProgress(20, new ProgressInfo(1, ProgressType.Finished));
            }
            worker.ReportProgress(20, new ProgressInfo(2, ProgressType.Processing));
            flag = false;
            message = "";
            try
            {
                directories = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories);
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        (new FileInfo(directory)).IsReadOnly = false;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                flag = true;
                message = exception.Message;
            }
            if (flag)
            {
                worker.ReportProgress(40, new ProgressInfo(2, ProgressType.Finished, true, message));
            }
            else
            {
                worker.ReportProgress(40, new ProgressInfo(2, ProgressType.Finished));
            }
            worker.ReportProgress(40, new ProgressInfo(3, ProgressType.Processing));
            flag = false;
            message = "";
            try
            {
                directories = Directory.GetFiles(projectPath, "*.scc", SearchOption.AllDirectories);
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        File.Delete(directory);
                    }
                }
                directories = Directory.GetFiles(projectPath, "*.vssscc", SearchOption.AllDirectories);
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        File.Delete(directory);
                    }
                }
                directories = Directory.GetFiles(projectPath, "*.vspscc", SearchOption.AllDirectories);
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        File.Delete(directory);
                    }
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                flag = true;
                message = exception.Message;
            }
            if (flag)
            {
                worker.ReportProgress(60, new ProgressInfo(3, ProgressType.Finished, true, message));
            }
            else
            {
                worker.ReportProgress(60, new ProgressInfo(3, ProgressType.Finished));
            }
            worker.ReportProgress(60, new ProgressInfo(4, ProgressType.Processing));
            flag = false;
            message = "";
            try
            {
                files = Directory.GetFiles(projectPath, "*.sln", SearchOption.AllDirectories);
                section = "(?is-mnx)(?<BeginSection>GlobalSection)\\(TeamFoundationVersionControl\\).*?End\\k<BeginSection>[\\n\\t\\s]*";
                directories = files;
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        content = File.ReadAllText(directory);
                        content = Regex.Replace(content, section, "", RegexOptions.IgnoreCase);
                        File.WriteAllText(directory, content);
                    }
                }
            }
            catch (Exception exception4)
            {
                exception = exception4;
                flag = true;
                message = exception.Message;
            }
            if (flag)
            {
                worker.ReportProgress(80, new ProgressInfo(4, ProgressType.Finished, true, message));
            }
            else
            {
                worker.ReportProgress(80, new ProgressInfo(4, ProgressType.Finished));
            }
            worker.ReportProgress(80, new ProgressInfo(5, ProgressType.Processing));
            try
            {
                files = Directory.GetFiles(projectPath, "*.csproj", SearchOption.AllDirectories);
                section = "<(?<BeginSection>Scc\\w+)>.*?</\\k<BeginSection>>[\\n\\t\\s]*";
                directories = files;
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        content = File.ReadAllText(directory);
                        content = Regex.Replace(content, section, "", RegexOptions.IgnoreCase);
                        File.WriteAllText(directory, content);
                    }
                }
                files = Directory.GetFiles(projectPath, "*.vbproj", SearchOption.AllDirectories);
                section = "<(?<BeginSection>Scc\\w+)>.*?</\\k<BeginSection>>[\\n\\t\\s]*";
                directories = files;
                for (i = 0; i < (int)directories.Length; i++)
                {
                    directory = directories[i];
                    if (!directory.Contains(backupPath))
                    {
                        content = File.ReadAllText(directory);
                        content = Regex.Replace(content, section, "", RegexOptions.IgnoreCase);
                        File.WriteAllText(directory, content);
                    }
                }
            }
            catch (Exception exception5)
            {
                exception = exception5;
                flag = true;
                message = exception.Message;
            }
            if (flag)
            {
                worker.ReportProgress(100, new ProgressInfo(5, ProgressType.Finished, true, message));
            }
            else
            {
                worker.ReportProgress(100, new ProgressInfo(5, ProgressType.Finished));
            }
        }
    }
}
