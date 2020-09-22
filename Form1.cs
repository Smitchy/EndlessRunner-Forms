using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public Form1()
        {
            InitializeComponent();
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

        private void Update()
        {
            var time = System.DateTime.Now.TimeOfDay;
            Console.WriteLine((time - lastUpdate).Ticks);
            lastUpdate = time;
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
