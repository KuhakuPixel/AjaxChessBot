using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace AjaxChessBot
{
    /// <summary>
    /// Use win32 api 'mouse_event' to make a click like left and right
    /// 
    /// mouse_event 's doc :   https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event 
    /// usage :                https://stackoverflow.com/questions/52679676/c-sharp-mouseclick-event-doesnt-fire-on-middle-click-or-right-click 
    /// </summary>
    class MouseOperation
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x0008,
            RightUp = 0x0010, 
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        
       

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        //TODO : dont hardcode this plz, only temporary solution

        public static void DragMouseLeftClick(MousePoint from ,MousePoint to)
        {
            MouseOperation.SetCursorPosition(from);

            //make a click once to activate the windows that is going to be clicked
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftDown);
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);
            //
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftDown);
            MouseOperation.SetCursorPosition(to);
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);

            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftDown);
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.LeftUp);

        }

        public static void DragMouseRightClick(MousePoint from ,MousePoint to)
        {
            MouseOperation.SetCursorPosition(from);

            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.RightDown);
            MouseOperation.SetCursorPosition(to);
            // add delay between down click and upclick
            // because without it, it seems to be too fast and might not be registered
            // on some program, but its kinda weird that left click works just fine, but not right click
            Thread.Sleep(500);
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.RightUp);


        }
        public static void MouseRightClick(MousePoint from )
        {
            MouseOperation.SetCursorPosition(from);

            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.RightDown);
            Thread.Sleep(1000);
            MouseOperation.MouseEvent(MouseOperation.MouseEventFlags.RightUp);


        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
