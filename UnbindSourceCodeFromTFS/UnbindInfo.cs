namespace UnbindSourceCodeFromTFS
{
    public class UnbindInfo
    {
        private string projectPath;

        private bool createBackup;

        public bool CreateBackup
        {
            get
            {
                return createBackup;
            }
        }

        public string ProjectPath
        {
            get
            {
                return projectPath;
            }
        }

        public UnbindInfo(string projectPath, bool createBackup)
        {
            this.projectPath = projectPath;
            this.createBackup = createBackup;
        }
    }
}
