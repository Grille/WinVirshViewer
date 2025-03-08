using Grille.IO;
using Grille.IO.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirshLib;

internal static class BinaryViewExtension
{
    public static void WritePassword(this BinaryViewWriter bw, string value)
    {
        bw.BeginCompressedSection(CompressionType.Deflate);

        bw.WriteLengthPrefix(value.Length, LengthPrefix.UIntSmart62);

        for (int i = 0; i < value.Length; i++) {
            bw.WriteLengthPrefix(value[i], LengthPrefix.UIntSmart62);
        }

        bw.EndCompressedSection();
    }

    public static string ReadPassword(this BinaryViewReader br)
    {
        br.BeginCompressedSection(CompressionType.Deflate);

        int length = (int)br.ReadLengthPrefix(LengthPrefix.UIntSmart62);
        var chars = new char[length];

        for (int i = 0; i < chars.Length; i++) {
            chars[i] = (char)br.ReadLengthPrefix(LengthPrefix.UIntSmart62);
        }

        br.EndCompressedSection();

        return new string(chars);
    }
}
