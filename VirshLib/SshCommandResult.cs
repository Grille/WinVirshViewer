using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VirshLib;

public class SshCommandResult : IReadOnlyList<string>
{
    readonly List<string> lines;

    public string Command { get; }

    public List<string> ContentLines {
        get {
            var newlines = new List<string>();
            for (int i = 1; i < lines.Count - 1; i++) {
                newlines.Add(lines[i]);
            }
            return newlines;
        }
    }

    public string Content {
        get {
            var sb = new StringBuilder();

            for (int i = 1; i < lines.Count - 1; i++) {
                sb.AppendLine(lines[i]);
            }
            return sb.ToString();
        }
    }


    public SshCommandResult(List<string> lines)
    {
        this.lines = lines;

        Command = lines[0];
    }

    public bool TryParseErrors(out string message)
    {
        var sb = new StringBuilder();
        bool success = true;

        foreach (var line in lines) {
            if (line.StartsWith("error:")) {
                success = false;
                sb.AppendLine(line.Split("error:", 2)[1].Trim());
            }
        }

        message = success ? null : sb.ToString();
        return !success;
    }


    public XmlDocument ToXml()
    {
        
        var xml = new XmlDocument();
        var content = Content;
        xml.LoadXml(content);
        return xml;
    }
    public string this[int index] => lines[index];

    public int Count => lines.Count;

    public IEnumerator<string> GetEnumerator()
    {
        return ((IEnumerable<string>)lines).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)lines).GetEnumerator();
    }
}
