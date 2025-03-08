using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace WinVirshViewer;

internal static class EmbeddedResources
{
    public static Bitmap VmStateShutOff { get; }

    public static Bitmap VmStatePaused { get; }

    public static Bitmap VmStateRunning { get; }

    public static Bitmap VmStateError { get; }

    public static Bitmap Pending { get; }

    static EmbeddedResources()
    {

        VmStateShutOff = Properties.Resources.vmstate_off;
        VmStatePaused = Properties.Resources.vmstate_pause;
        VmStateRunning = Properties.Resources.vmstate_run;
        VmStateError = Properties.Resources.vmstate_error;

        Pending = Properties.Resources.pending;
    }
}
