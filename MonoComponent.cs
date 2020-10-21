﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    public abstract class MonoComponent
    {
        private MonoEntity owner;
        public MonoEntity Owner { get => owner; set => owner = value; }
        private int priority;
        public int Priority { get => priority; }
        public bool isActive;

        public abstract void FixedUpdate();
        public abstract void Update();

        protected MonoComponent(int prio)
        {
            this.priority = prio;
            isActive = true;
        }
    }


    public class PlayerMovement : MonoComponent
    {
        public float speed;
        public PlayerMovement(int prio) : base( prio)
        {
            speed = 0.5f;
        }

        public override void FixedUpdate()
        {
 
        }
        public override void Update()
        {
            if (Input.OnKeyPressed(Keys.Space))
            {
                Owner.Destroy();
            }
            if (Input.OnKeyHeld(Keys.W))
            {                
                Owner.transform.position.Y -= speed * Time.deltaTime;
            }
            if (Input.OnKeyHeld(Keys.A))
            {
                Owner.transform.position.X -= speed * Time.deltaTime;
                Owner.transform.rotation = 180;
            }
            if (Input.OnKeyHeld(Keys.S))
            {
                Owner.transform.position.Y += speed * Time.deltaTime;
            }
            if (Input.OnKeyHeld(Keys.D))
            {
                Owner.transform.position.X += speed * Time.deltaTime;
                Owner.transform.rotation = 0;
            }
        }
    }

    public class Obstacle : MonoComponent
    {

        public float speed;
        public Obstacle(int prio) : base(prio)
        {
            speed = .2f;
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Update()
        {
            Owner.transform.position.X -= speed * Time.deltaTime;
            if (Owner.transform.position.X < 0)
                Owner.Destroy();
            
            
        }
    }

    public class ObstacleSpawner : MonoComponent
    {
        Image bullet = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "../../images/feelsgoodman.png");
        Random rand = new Random();
        public ObstacleSpawner(int prio) : base(prio)
        {
            for (int i = 0; i < 30; i++)
            {
                CreateObstacle(new Vector2(1000, rand.Next(0, 1000)));
            }
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            //CreateObstacle(new Vector2(1000,rand.Next(0,1000)));
           
        }
        private MonoEntity CreateObstacle(Vector2 pos)
        {
            Obstacle obs = new Obstacle(1);
            MonoEntity ent = new MonoEntity("bullet");
            ent.transform.position = pos;
            ent.transform.scale = Vector2.One * 100;
            ent.AddComponent(new RenderComponent(1, bullet));
            ent.AddComponent(obs);

            return ent;
        }
    }
}
