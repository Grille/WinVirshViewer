using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirshLib;

public enum ConnectState
{
    None,
    Connecting,
    SSH,
    Password,
    AuthenticationFailed,
    Failed,
}

[Flags]
public enum RefreshType
{
    None,
    Self = 1,
    Owner = 2,
}

public enum VmState
{
    Running,
    Idle,
    Paused,
    Shutdown,
    ShutOff,
    Crashed,
    Dying,
    PmSuspended,
    Invalid,
}

public static class VmStateConverter
{
    public static VmState ConvertToVmState(this string input) => input.ToLower().Trim(' ', '\r') switch {
        "running" => VmState.Running,
        "idle" => VmState.Idle,
        "paused" => VmState.Paused,
        "in shutdown" => VmState.Shutdown,
        "shutdown" => VmState.Shutdown,
        "shutoff" => VmState.ShutOff,
        "shut off" => VmState.ShutOff,
        "crashed" => VmState.Crashed,
        "dying" => VmState.Dying,
        "pmsuspended" => VmState.PmSuspended,
        _ => throw new ArgumentOutOfRangeException(input),
    };


    public static string ConvertToString(this VmState input) => input switch {
        VmState.Running => "running",
        VmState.Idle => "idle",
        VmState.Paused => "paused",
        VmState.Shutdown => "shutdown",
        VmState.ShutOff => "shut off",
        VmState.Crashed => "crashed",
        VmState.Dying => "dying",
        VmState.PmSuspended => "pmsuspended",
        _ => throw new ArgumentOutOfRangeException(nameof(input)),
    };
}


