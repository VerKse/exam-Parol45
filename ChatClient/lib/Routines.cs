using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChatClient.lib
{
    static class Routines
    {
        public static void PrintToMessageBox(string message, RichTextBoxEx messageBox)
        {
             messageBox.BeginInvoke(new Action(() => messageBox.AppendText("\n" + message)));
             messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
             messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
        }
    }
}
