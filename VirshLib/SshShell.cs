using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirshLib;

public class SshShell : IDisposable
{
    private readonly TaskQueue _queue;

    private int AccessCount = 0;

    private string promt = null;

    private ShellStream _shell;
    private bool disposedValue;

    public SshShell(ShellStream connection)
    {
        _shell = connection;
        _queue = new TaskQueue();
        InitShell();
    }


    private Task<SshCommandResult> InitShell()
    {
        var task = new Task<SshCommandResult>(() => {

            //SendCommandAsync(null);
            var result = ReadAll(100);
            var lines = result.Split("\r\n").ToList();

            promt = lines.Last();

            //Console.WriteLine("----------------------<init0>");
            //Console.WriteLine(result);
            //Console.WriteLine("----------------------<init1>");

            return new SshCommandResult(lines);
        });

        _queue.Enqueue(task);

        return task;
    }

    public Task<SshCommandResult> Command(string command)
    {
        var task = new Task<SshCommandResult>(() => {
            AccessCount += 1;
            if (AccessCount != 1)
                throw new Exception();

            WriteLine(command);
            var result = ReadLines(1);

            //Console.WriteLine("----------------------<0>");
            //Console.WriteLine(string.Join('\n', result));
            //Console.WriteLine("----------------------<1>");

            AccessCount -= 1;

            return new SshCommandResult(result);
        });

        _queue.Enqueue(task);

        return task;
    }


    private void WriteLine(string command)
    {
        //Connection.AssertIsConnected();


        _shell.WriteLine(command);
        _shell.Flush();

    }

    private void ClearShell()
    {
        //lock (_lock) {
        while (true) {
            var result = _shell.ReadByte();
            //Console.WriteLine(result);
            //Console.WriteLine($"{shell.CanRead} {result} {(!shell.CanRead || result == -1)}");
            if (!_shell.CanRead || result == -1) {
                break;
            }
        }
    }

    private string ReadLine(int timeoutMs = 50)
    {
        var timeout = TimeSpan.FromMilliseconds(timeoutMs);
        var lastRead = DateTime.Now;

        var chars = new List<char>();

        //lock (_lock) {
        while (true) {
            var result = _shell.ReadByte();

            //Console.Write((char)result);
            //Console.WriteLine($"{_shell.CanRead} {result} {(!_shell.CanRead || result == -1)}");

            if ((char)result == '\r') {
                var next = _shell.ReadByte();
                break;
                //if ((char)next == '\n') {
                //    break;
                //}
                //else {
                //    _shell.Seek(-1, SeekOrigin.Current);
                //}
            }

            if (!_shell.CanRead || result == -1) {
                var delta = DateTime.Now - lastRead;
                //Console.WriteLine($"Exit {delta.TotalMilliseconds} > {timeout.TotalMilliseconds}");
                if (delta > timeout) {
                    break;
                }
            }
            else {
                chars.Add((char)result);
                lastRead = DateTime.Now;
            }
        }

        return chars.Count > 0 ? new string(chars.ToArray()) : null;
    }

    private string ReadAll(int timeoutMs = 50)
    {
        var timeout = TimeSpan.FromMilliseconds(timeoutMs);
        var lastRead = DateTime.Now;

        var chars = new List<char>();
        while (true) {
            var result = _shell.ReadByte();
            if (!_shell.CanRead || result == -1) {
                if (DateTime.Now - lastRead > timeout) {
                    break;
                }
            }
            else {
                chars.Add((char)result);
                lastRead = DateTime.Now;
            }
        }

        return new string(chars.ToArray());
    }

    private List<string> ReadLines(int timeout = 50)
    {
        var lines = new List<string>();
        while (true) {
            var line = ReadLine(timeout);
            if (line == null)
                continue;

            lines.Add(line);
            if (line == promt)
                break;
        }
        return lines;
    }

    public void Dispose()
    {
        if (disposedValue)
            return;

        _shell.Dispose();

        disposedValue = true;
    }
}
