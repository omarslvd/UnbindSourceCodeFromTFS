using System.Windows.Forms;

namespace UnbindSourceCodeFromTFS
{
    public static class MessageBoxEx
    {
        public static DialogResult Show(string text)
        {
            return MessageBoxEx.Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string text, string caption)
        {
            return MessageBoxEx.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBoxEx.Show(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBoxEx.Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(text, caption, buttons, icon, defaultButton);
        }
    }
}
