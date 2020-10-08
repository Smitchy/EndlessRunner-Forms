using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();

            form.Show();

            form.Closed += (sender, args) => form.running = false;

            MonoEntity entity = new MonoEntity();
            entity.transform.scale = Vector2.One * 100;
            entity.AddComponent(new RenderComponent(0, entity, Image.FromFile("C:/Users/Student/Documents/GitHub/EndlessRunner-Forms/images/feelsgoodman.png")));
            entity.AddComponent(new MockComponent(0, entity));

            form.RunGameLoop();
        }
    }
}
