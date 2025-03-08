using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirshLib;

namespace WinVirshViewer.WinForms;

public static class ResultMessageBox
{
    public static async void ShowOnError(this Task<SshCommandResult> task, UpdateableForm owner, string title)
    {
        var res = await task;
        owner.Invalidate();
        if (res.TryParseErrors(out var message)) {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        owner.UpdateContent();
    }
}
