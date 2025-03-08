using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Grille.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace VirshLib;

public class VirshHost : RemoteObject, IViewObject
{
    public string User = "user";
    public string Address = "localhost";
    public string Password = "";
    public string Name = "";
    public bool SavePassword = false;
    public bool AutoConnect = true;

    public bool Visible = true;

    public string FullAddress => $"{User}@{Address}";

    public VirtualMachineList VirtualMachines { get; }

    public bool Refreshing {  get; private set; }

    public ConnectState ConnectionState {
        get => Connection.ConnectResult;
    }

    public VirshHost(BinaryViewReader br) : base(new SshConnection())
    {
        ReadFromView(br);

        RefreshConnection();

        VirtualMachines = new VirtualMachineList(this);
    }

    public VirshHost(string user, string address, string password = null) : base(new SshConnection())
    {
        User = user;
        Address = address;
        Password = password;

        RefreshConnection();

        VirtualMachines = new VirtualMachineList(this);
    }

    public void RefreshConnection()
    {
        if (Refreshing == true)
            return;

        Refreshing = true;

        Task.Run(() => {
            Connection.DisposeConnection();
            Connection.SetLogin(User, Address, Password);
            Connection.Connect();
        });

        Refreshing = false;
    }

    protected override void OnPull()
    {
        //if (AutoConnect && !Connection.IsConnected)
            //ConnectAsync().Wait();
    }

    public void ReadFromView(BinaryViewReader br)
    {
        User = br.ReadString();
        Address = br.ReadString();
        SavePassword = br.ReadBoolean();
        Password = SavePassword ? br.ReadPassword() : "";
        AutoConnect = br.ReadBoolean();
    }

    public void WriteToView(BinaryViewWriter bw)
    {
        bw.WriteString(User); 
        bw.WriteString(Address);
        bw.WriteBoolean(SavePassword);
        if (SavePassword) {
            bw.WritePassword(Password);
        }
        bw.WriteBoolean(AutoConnect);
    }
}
