using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
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
            Image img = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "../../images/feelsgoodman.png");         
            MonoEntity entity = new MonoEntity("player");
            entity.transform.scale = Vector2.One * 100;           
            new RenderComponent(20, entity, img);
            new CircleCollider(0, entity, img.Width / 2, new List<int>(1));
            new PlayerMovement(0, entity);
            MonoEntity mapEntity = new MonoEntity("map");
            new ObstacleSpawner(1, mapEntity);
            MonoEntity scoreEnt = new MonoEntity("score");            
            new ScoreComp(21, scoreEnt);
            
            form.RunGameLoop();
           
        }
    }
}
