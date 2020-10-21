﻿using System;
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
            Image img2 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "../../images/feelsgoodman.png");
            MonoEntity entity = new MonoEntity("player");
            entity.transform.scale = Vector2.One * 100;
            
            entity.AddComponent(new RenderComponent(20, img));
            entity.AddComponent(new PlayerMovement(0));
            MonoEntity mapEntity = new MonoEntity("map");
            //mapEntity.transform.position = new Vector2(600, 400);
            //mapEntity.transform.scale = Vector2.One * 100;
            mapEntity.AddComponent(new ObstacleSpawner(1));
            mapEntity.AddComponent(new RenderComponent(1, img2));

            form.RunGameLoop();
           
        }
    }
}
