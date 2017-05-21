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
        private static extern uint mciGetDeviceID(string lpstrName);

        public event Action<int> PlayEnd;

        IntPtr handle = IntPtr.Zero;
        SoundPlayerWindow win = null;

        public SoundPlayer() { }

        public void Init(Control parent)
        {
            handle = parent.Handle;
            win = new SoundPlayerWindow();
            win.AssignHandle(handle);
            win.PlayEnd += id => PlayEnd?.Invoke(id);
        }

        public int GetDeviceID(string name)
        {
            return (int)mciGetDeviceID(name);
        }

        public int Open(string file, string name)
        {
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

            if (0 > mciSendString(cmd, null, 0, handle))
                return -1;

            return GetDeviceID(name);
        }

        public int Play(string name)
        {
            var cmd = $"play {name} notify";
            return mciSendString(cmd, null, 0, handle);
        }

        public int Play(string name, string from, string to)
        {
            var cmd = $"play {name} from {from} to {to} notify";
            return mciSendString(cmd, null, 0, handle);
        }

        public int Stop(string name)
        {
            var cmd = $"stop {name}";
            return mciSendString(cmd, null, 0, handle);
        }

        public int Close(string name)
        {
            var cmd = $"close {name}";
            return mciSendString(cmd, null, 0, handle);
        }

        public int CloseAll()
        {
            var cmd = $"close all";
            return mciSendString(cmd, null, 0, handle);
        }

        public int Pause(string name)
        {
            var cmd = $"pause {name}";
            return mciSendString(cmd, null, 0, handle);
        }

        public int Resume(string name)
        {
            var cmd = $"resume {name}";
            return mciSendString(cmd, null, 0, handle);
        }

        public int Seek(string name, string pos)
        {
            var cmd = $"seek {name} to {pos}";
            return mciSendString(cmd, null, 0, handle);
        }

        public string Status(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} mode";
            mciSendString(cmd, sb, sb.Capacity, handle);
            return sb.ToString();
        }

        public string Position(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} position";
            mciSendString(cmd, sb, sb.Capacity, handle);
            return sb.ToString();
        }

        public string TimeFormat(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} time format";
            mciSendString(cmd, sb, sb.Capacity, handle);
            return sb.ToString();
        }

        public int SetTimeFormat(string name, string format)
        {
            var cmd = $"set {name} time format {format}";
            return mciSendString(cmd, null, 0, handle);
        }

        public string Length(string name)
        {
            var sb = new StringBuilder(100);
            var cmd = $"status {name} length";
            mciSendString(cmd, sb, sb.Capacity, handle);
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
