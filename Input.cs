using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    public static class Input
    {
        private static List<Keys> keysPressed = new List<Keys>();
        private static List<Keys> keysPressedLast = new List<Keys>();
        private static List<Keys> keysHeld = new List<Keys>();
        private static List<Keys> keysReleased = new List<Keys>();
        private static List<Keys> keysReleasedLast = new List<Keys>();

        public static void KeysPressedSetter(object sender, KeyEventArgs e)
        {
            if (!keysHeld.Contains(e.KeyCode))
            {
                keysPressed.Add(e.KeyCode);
                keysHeld.Add(e.KeyCode);
            }
        }
        public static void KeysReleasedSetter(object sender, KeyEventArgs e)
        {
            if (keysHeld.Contains(e.KeyCode))
            {
                keysHeld.Remove(e.KeyCode);
            }
            keysReleased.Add(e.KeyCode);
        }

        public static void HandleInput()
        {
            keysPressedLast = new List<Keys>(keysPressed);
            keysPressed.Clear();
            keysReleasedLast = new List<Keys>(keysReleased);
            keysReleased.Clear();
        }
        public static bool OnKeyPressed(Keys k)
        {
            return keysPressedLast.Contains(k);
        }
        public static bool OnKeyHeld(Keys k)
        {
            return keysHeld.Contains(k);
        }
        public static bool OnKeyReleased(Keys k)
        {
            return keysReleasedLast.Contains(k);
        }
    }
}
