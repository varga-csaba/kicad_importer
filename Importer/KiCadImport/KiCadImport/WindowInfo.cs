using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiCadImport
{
    public class WindowInfo
    {
        private int hwnd;
        private string title;

        public WindowInfo(int hwnd, string title)
        {
            this.hwnd = hwnd;
            this.title = title;
        }

        public int Hwnd
        {
            get { return hwnd; }
        }

        public string Title
        {
            get { return title; }
        }
    }
}
