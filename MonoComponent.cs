using System;
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

        protected MonoComponent(int prio, MonoEntity owner)
        {
            this.priority = prio;
            owner.AddComponent(this);
            isActive = true;
        }
    }


    public class PlayerMovement : MonoComponent
    {
        public float speed;
        public PlayerMovement(int prio, MonoEntity owner) : base(prio, owner)
        {
            speed = 0.5f;
            ((CircleCollider)Owner.GetComponent(typeof(CircleCollider))).OnCollisionEvent += OnCollision;
        }

        public override void FixedUpdate()
        {

        }

        public void OnCollision(CircleCollider other)
        {
            Form1.Reset();

        }


        public override void Update()
        {
            if (Input.OnKeyHeld(Keys.W))
            {
                if (Owner.transform.position.Y > 0)
                    Owner.transform.position.Y -= speed * Time.deltaTime;
            }
            if (Input.OnKeyHeld(Keys.A))
            {
                if (Owner.transform.position.X > 0)
                    Owner.transform.position.X -= speed * Time.deltaTime;
            }
            if (Input.OnKeyHeld(Keys.S))
            {
                if (Owner.transform.position.Y < 900)
                    Owner.transform.position.Y += speed * Time.deltaTime;
            }
            if (Input.OnKeyHeld(Keys.D))
            {
                if (Owner.transform.position.X < 900)
                    Owner.transform.position.X += speed * Time.deltaTime;
            }
        }
    }

    public class Obstacle : MonoComponent
    {

        public double speed;
        private DateTime startTimeSeconds;
        public Obstacle(int prio, MonoEntity owner) : base(prio, owner)
        {
            startTimeSeconds = DateTime.Now;
            speed = 1;
            speed /= RandomHelper.rand.Next(5, 20);
        }

        public override void FixedUpdate()
        {

        }

        public override void Update()
        {
            Owner.transform.position.X -= (float)speed * Time.deltaTime;
            if (Owner.transform.position.X < 0)
            {
                Owner.transform.position.X = 1000;
                Owner.transform.position.Y = RandomHelper.rand.Next(0, 900);
                speed = Math.Log(DateTime.Now.Subtract(startTimeSeconds).TotalSeconds) + 0.5;
                speed /= RandomHelper.rand.Next(5, 20);
                ScoreComp.instance.IncrementScore();
            }


        }
    }

    public class ObstacleSpawner : MonoComponent
    {
        Image bullet = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "../../images/boulder.png");
        public ObstacleSpawner(int prio, MonoEntity owner) : base(prio, owner)
        {
            for (int i = 0; i < 15; i++)
            {
                CreateObstacle(new Vector2(1000, RandomHelper.rand.Next(0, 900)));
            }
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {

        }
        private MonoEntity CreateObstacle(Vector2 pos)
        {
            MonoEntity ent = new MonoEntity("bullet");
            ent.transform.position = pos;
            ent.transform.scale = Vector2.One * 100;
            new Obstacle(1, ent);
            new RenderComponent(1, ent, bullet);
            new CircleCollider(Priority, ent, bullet.Width / 2, new List<int>(0));

            return ent;
        }
    }

    public class ScoreComp : RenderComponent
    {
        public static ScoreComp instance;
        private int score;
        private readonly string scoreText;
        public ScoreComp(int prio, MonoEntity owner) : base(prio, owner, null)
        {
            if (instance == null || instance != this)
            {
                instance = this;
            }
            scoreText = "Your score: ";
            ResetScore();
        }
        public void IncrementScore()
        {

            score++;
        }

        public void ResetScore()
        {
            score = 0;
        }

        public override void Draw(Graphics g)
        {
            g.DrawString(scoreText + score.ToString(), new Font(FontFamily.GenericSerif, 24), new SolidBrush(Color.Blue), new Point((int)Owner.transform.position.X, (int)Owner.transform.position.Y));
        }
        public override void FixedUpdate()
        {

        }

        public override void Update()
        {

        }
    }

    public class ResetListener : RenderComponent
    {
        public ResetListener(int prio, MonoEntity owner) : base(prio, owner, null)
        {

        }

        public override void Draw(Graphics g)
        {
            if (Input.OnKeyReleased(Keys.Space))
            {
                Console.Write("Test");
                Image img = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "../../images/feelsgoodman.png");
                Image img2 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "../../images/feelsgoodman.png");
                MonoEntity entity = new MonoEntity("player");
                entity.transform.scale = Vector2.One * 100;
                new RenderComponent(20, entity, img);
                new CircleCollider(0, entity, img.Width / 2, new List<int>(1));
                new PlayerMovement(0, entity);
                MonoEntity mapEntity = new MonoEntity("map");
                new ObstacleSpawner(1, mapEntity);
                new RenderComponent(1, mapEntity, img2);
                MonoEntity scoreEnt = new MonoEntity("score");
                new ScoreComp(21, scoreEnt);

                Owner.RemoveComponent(this);
            }
            g.DrawString("FEELSBADMAN", new Font(FontFamily.GenericSerif, 50), new SolidBrush(Color.Red), new Point((int)Owner.transform.position.X, (int)Owner.transform.position.Y));
        }
        public override void FixedUpdate()
        {

        }

        public override void Update()
        {

        }
    }
}
