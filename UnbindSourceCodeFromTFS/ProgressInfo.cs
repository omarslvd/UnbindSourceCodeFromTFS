namespace UnbindSourceCodeFromTFS
{
    public enum ProgressType : short
    {
        Processing,
        Finished
    }

    public class ProgressInfo
    {
        private int step;

        private ProgressType progressType;

        private bool error;

        private string errorMessage;

        public bool Error
        {
            get
            {
                return error;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
        }

        public ProgressType ProgressType
        {
            get
            {
                return progressType;
            }
        }

        public int Step
        {
            get
            {
                return step;
            }
        }

        public ProgressInfo(int step, ProgressType progressType)
        {
            this.step = step;
            this.progressType = progressType;
            this.error = false;
            this.errorMessage = "";
        }

        public ProgressInfo(int step, ProgressType progressType, bool error, string errorMessage)
        {
            this.step = step;
            this.progressType = progressType;
            this.error = error;
            this.errorMessage = errorMessage;
        }
    }
}
