using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GottaniRPG
{
    public class SoundPlayer : IDisposable
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringW", CharSet = CharSet.Unicode)]
        private static extern int mciSendString(string lpszCommand, StringBuilder lpszReturnString, int cchReturn, IntPtr hwndCallback);

        [DllImport("winmm.dll", EntryPoint = "mciGetDeviceIDW", CharSet = CharSet.Unicode)]
        private static extern int mciGetDeviceID(string lpstrName);

        public event Action<int> PlayEnd;

        Control parent = null;
        SoundPlayerWindow win = null;

        public SoundPlayer() { }

        public void Init(Control parent)
        {
            this.parent = parent;
            win = new SoundPlayerWindow();
            win.AssignHandle(parent.Handle);
            win.PlayEnd += id => PlayEnd?.Invoke(id);
        }

        public int GetDeviceID(string name)
        {
            var result = 0;
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciGetDeviceID(name);
            }));
            return result;
        }

        public int Open(string file, string name)
        {
            var result = 0;
            var cmd = "";
            switch (Path.GetExtension(file))
            {
                case ".mp3":
                    cmd = $"open \"{file}\" alias {name} type MPEGVideo";
                    break;
                case ".wav":
                    cmd = $"open \"{file}\" alias {name} type waveaudio";
                    break;
                case ".mid":
                    cmd = $"open \"{file}\" alias {name} type sequencer";
                    break;
                default:
                    cmd = $"open \"{file}\" alias {name}";
                    break;
            }

            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));

            if (0 > result)
                return -1;

            return GetDeviceID(name);
        }

        public int Play(string name)
        {
            var result = 0;
            var cmd = $"play {name} notify";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int Play(string name, string from, string to)
        {
            var result = 0;
            var cmd = $"play {name} from {from} to {to} notify";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int Stop(string name)
        {
            var result = 0;
            var cmd = $"stop {name}";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int Close(string name)
        {
            var result = 0;
            var cmd = $"close {name}";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int CloseAll()
        {
            var result = 0;
            var cmd = $"close all";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int Pause(string name)
        {
            var result = 0;
            var cmd = $"pause {name}";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int Resume(string name)
        {
            var result = 0;
            var cmd = $"resume {name}";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public int Seek(string name, string pos)
        {
            var result = 0;
            var cmd = $"seek {name} to {pos}";

            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public string Status(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} mode";
            parent.Invoke((MethodInvoker)(() =>
            {
                mciSendString(cmd, sb, sb.Capacity, parent.Handle);
            }));
            return sb.ToString();
        }

        public string Position(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} position";
            parent.Invoke((MethodInvoker)(() =>
            {
                mciSendString(cmd, sb, sb.Capacity, parent.Handle);
            }));
            return sb.ToString();
        }

        public string TimeFormat(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} time format";
            parent.Invoke((MethodInvoker)(() =>
            {
                mciSendString(cmd, sb, sb.Capacity, parent.Handle);
            }));
            return sb.ToString();
        }

        public int SetTimeFormat(string name, string format)
        {
            var result = 0;
            var cmd = $"set {name} time format {format}";
            parent.Invoke((MethodInvoker)(() =>
            {
                result = mciSendString(cmd, null, 0, parent.Handle);
            }));
            return result;
        }

        public string Length(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} length";
            parent.Invoke((MethodInvoker)(() =>
            {
                mciSendString(cmd, sb, sb.Capacity, parent.Handle);
            }));
            return sb.ToString();
        }

        public void Dispose()
        {
            if (win != null)
                win.ReleaseHandle();
            CloseAll();
        }

        protected class SoundPlayerWindow : NativeWindow
        {
            public event Action<int> PlayEnd;

            protected override void WndProc(ref Message m)
            {
                const int MM_MCINOTIFY = 0x3B9;
                const int MCI_NOTIFY_SUCCESSFUL = 1;
                switch (m.Msg)
                {
                    case MM_MCINOTIFY:
                        if ((int)m.WParam == MCI_NOTIFY_SUCCESSFUL)
                        {
                            PlayEnd?.Invoke((int)m.LParam);
                        }
                        break;
                }
                base.WndProc(ref m);
            }
        }
    }
}
