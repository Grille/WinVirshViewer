using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using Grille.IO;

namespace VirshLib;

public class VirshHostList : List<VirshHost>, IViewObject
{
    private const uint fmagic = 0xAF420001;

    public string FilePath { get; set; }

    public VirshHostList(string path)
    {
        FilePath = path;
        if (File.Exists(path)) {
            Load();
        }
    }

    public void Load()
    {
        using var br = new BinaryViewReader(FilePath) {
            LengthPrefixMaxValue = 1024,
            LengthPrefix = LengthPrefix.Int32,
            Encoding = Encoding.UTF8,
        };

        br.ReadToIView(this);
    }

    public void Save()
    {
        using var bw = new BinaryViewWriter(FilePath) {
            LengthPrefix = LengthPrefix.Int32,
            Encoding = Encoding.UTF8,
        };

        bw.WriteIView(this);
    }


    public void ReadFromView(BinaryViewReader br)
    {
        if (br.ReadUInt32() != fmagic)
            throw new InvalidDataException("Unknown file identifier");

        int count = br.ReadInt32();
        for (int i = 0;i < count; i++) {
            var item = new VirshHost(br);
            Add(item);
        }
    }

    public void WriteToView(BinaryViewWriter bw)
    {
        bw.WriteUInt32(fmagic);
        bw.WriteInt32(Count);
        foreach (var item in this) {
            bw.WriteIView(item);
        }
    }
}
