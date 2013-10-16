using System;
using System.Runtime.InteropServices;

namespace LeapSlideShow
{
    class MouseCursor
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        public static void MoveCursor(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x,y);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void Draw(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void MouseUp() 
        {
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }


        //public static void sendMouseLeftclick(uint x, uint y)
        //{
        //    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        //}

        //public static void sendMouseRightclick(uint x, uint y)
        //{
        //    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        //}

        //public static void sendMouseDoubleClick(uint x, uint y)
        //{
        //    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);

        //    System.Threading.Thread.Sleep(150);

        //    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        //}

        //public static void sendMouseRightDoubleClick(uint x, uint y)
        //{
        //    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);

        //    System.Threading.Thread.Sleep(150);

        //    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        //}

        //public static void sendMouseDown()
        //{
        //    mouse_event(MOUSEEVENTF_LEFTDOWN, 50, 50, 0, 0);
        //}


        //public static void sendMouseUp()
        //{
        //    mouse_event(MOUSEEVENTF_LEFTUP, 50, 50, 0, 0);
        //}
    }
}
