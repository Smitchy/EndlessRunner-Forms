using System;
using System.Collections.Generic;
using System.Linq;
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

            entity.AddComponent(new MockComponent(5));
            entity.AddComponent(new MockComponent(2));
            entity.AddComponent(new MockComponent(3));
            entity.AddComponent(new MockComponent(1));
            entity.AddComponent(new MockComponent(5));

            form.RunGameLoop();
        }
    }
}
