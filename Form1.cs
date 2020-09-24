using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    public partial class Form1 : Form
    {

        public bool running;

        private List<Keys> keysPressed = new List<Keys>();

        private List<Keys> keysUnPressed = new List<Keys>();

        private TimeSpan lastUpdate = System.DateTime.Now.TimeOfDay;

        private bool spacePressed;

        private TimeSpan fixedUpdateInterval = new TimeSpan(166667);

        private TimeSpan timeForFixedUpdates = new TimeSpan(0);

        private static Dictionary<int, List<GMD2Project___endless_running.IComponent>> comps = new Dictionary<int, List<IComponent>>();

        private int maxprio = 9;

        public Form1()
        {
            InitializeComponent();
        }

        public static void AddComponent(IComponent component, int prio)
        {
            if(!comps.ContainsKey(prio))
            {
                comps[prio] = new List<IComponent>();
            }
            comps[prio].Add(component);
        }

        private void KeysPressedSetter(object sender, KeyEventArgs e)
        {
            keysPressed.Add(e.KeyCode);
        }

        private void KeysUnPressedSetter(object sender, KeyEventArgs e)
        {
            keysUnPressed.Add(e.KeyCode);
        }

        internal void RunGameLoop()
        {
            running = true;

            while (running)
            {
                HandleInput();
                FixedUpdate();
                Update();
                Render();
            }
        }

        private void HandleInput()
        {
            List<Keys> tempInputs = new List<Keys>(keysPressed);
            keysPressed.Clear();
            foreach(Keys k in tempInputs)
            {
                switch (k)
                {
                    //mby add other keys but prob not.
                    case Keys.Space:
                        spacePressed = true;
                        break;
                }
            }

            tempInputs = new List<Keys>(keysUnPressed);
            keysUnPressed.Clear();
            foreach(Keys keys in tempInputs)
            {
                switch (keys)
                {
                    case Keys.Space:
                        spacePressed = false;
                        break;
                }
            }
        }

        private void FixedUpdate()
        {
            timeForFixedUpdates += System.DateTime.Now.TimeOfDay - lastUpdate;
            while (timeForFixedUpdates > fixedUpdateInterval)
            {
                for(int i = 0;  i < maxprio; i++)
                {
                    if (comps.ContainsKey(i))
                    {
                        Console.WriteLine("prio: " + i);
                        foreach(IComponent comp in comps[i])
                        {
                            comp.FixedUpdate();
                        }
                    }
                }

                Console.WriteLine("Fixed Update: " + timeForFixedUpdates);
                timeForFixedUpdates -= fixedUpdateInterval;
            }
            lastUpdate = System.DateTime.Now.TimeOfDay;
        }

        private void Update()
        {
            var time = System.DateTime.Now.TimeOfDay;
            //Console.WriteLine("Update: " + (time - lastUpdate).Ticks);
        }

        private void Render()
        {
            Bitmap bitmap = new Bitmap(canvas.Width, canvas.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(spacePressed ? new SolidBrush(Color.Crimson) : new SolidBrush(Color.Blue), 100, 100, 200, 200);

            canvas.Image = bitmap;

            Application.DoEvents();
        }
    }
}
