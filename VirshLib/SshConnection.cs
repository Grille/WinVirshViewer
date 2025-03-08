using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VirshLib;

public class SshConnection : IDisposable
{
    private bool disposedValue;

    public string User { get; private set; } = "user";
    public string Address { get; private set; } = "localhost";
    public string Password { get; private set; } = null;

    /// <summary>
    /// Shell used to issue blocking commands.
    /// </summary>
    public SshShell MainShell { get; private set; }

    /// <summary>
    /// Shell used to query info.
    /// </summary>
    public SshShell InfoShell { get; private set; }

    public ConnectState ConnectResult { get; private set; } = ConnectState.None;

    public SshClient Client { get; private set; }


    public SshConnection(string user = "root", string address = "localhost", string password = null)
    {
        SetLogin(user, address, password);
    }


    public void SetLogin(string user = "root", string address = "localhost", string password = null)
    {
        User = user;
        Address = address;
        Password = password;
    }

    public bool IsConnected {
        get { return Client != null && Client.IsConnected; }
    }

    public void DisposeConnection()
    {
        if (Client != null) {
            if (Client.IsConnected)
                Client.Disconnect();
            Client.Dispose();
            Client = null;
        }

        ConnectResult = ConnectState.None;
    }

    public ConnectState Connect()
    {
        ConnectResult = ConnectState.Connecting;
        ConnectResult = TryConnectionMethods();

        if (IsConnected) {
            MainShell = CreateShell();
            InfoShell = CreateShell();
        }

        return ConnectResult;
    }

    public SshShell CreateShell()
    {
        return new SshShell(Client.CreateShellStream("", 100, 100, 100, 100, ushort.MaxValue));
    }


    private ConnectState TryConnectionMethods()
    {
        string sshKeyFolder = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".ssh");
        var filesInSshFolder = Directory.GetFiles(sshKeyFolder);

        var privateKeys = new List<PrivateKeyFile>();
        foreach (var file in filesInSshFolder) {
            try {
                var privateKey = new PrivateKeyFile(file);
                privateKeys.Add(privateKey);
            }
            catch (SshException) {/*not an valid private-key -> continue*/}
        }

        try {
            // over publickey
            if (privateKeys.Count > 0) {
                Client = new SshClient(Address, User, privateKeys.ToArray());
                try {
                    Client.Connect();
                    return ConnectState.SSH;
                }
                catch (SshAuthenticationException) {/*Permission denied (publickey)*/}
            }

            // over passsword
            if (Password != null) {
                Client = new SshClient(Address, User, Password);
                try {
                    Client.Connect();
                    return ConnectState.Password;
                }
                catch (SshAuthenticationException) {/*Permission denied (password)*/}
            }
            return ConnectState.AuthenticationFailed;
        }
        catch {
            return  ConnectState.Failed;
        }


    }

    public async Task AwaitReady()
    {
        while (true) {
            if (IsConnected && MainShell != null && InfoShell != null)
                return;
            await Task.Delay(500);
        }
    }

    public void AssertIsConnected()
    {
        if (ConnectResult == ConnectState.None)
            throw new Exception("ConnectResult is None.");

        if (ConnectResult == ConnectState.Failed)
            throw new Exception("ConnectResult is Failed.");

        if (MainShell == null)
            throw new NullReferenceException(nameof(MainShell));

        if (InfoShell == null)
            throw new NullReferenceException(nameof(InfoShell));
    }

    /*
    public async Task<SshCommandResult> Command(string command)
    {
        await AwaitReady();

        return await MainShell.Command(command);
    }

    public async Task<SshCommandResult> LongCommand(string command)
    {
        await AwaitReady();

        using var shell = CreateShell();
        var task = shell.Command(command);
        return await task;
    }
    */

    public void Dispose()
    {
        if (disposedValue)
            return;

        MainShell.Dispose();
        InfoShell.Dispose();

        Client.Dispose();

        disposedValue = true;
    }
}
