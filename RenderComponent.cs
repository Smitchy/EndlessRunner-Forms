using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    public class RenderComponent : MonoComponent
    {
        public Image sprite { get; set; }
        public float rotation = -1f;

        public RenderComponent(int prio, MonoEntity owner, Image s) : base(prio, owner)
        {
            sprite = s;
        }
        public virtual void Draw(Graphics g)
        {
            g.DrawImage(sprite, Owner.transform.position.X - (sprite.Width/2), Owner.transform.position.Y - (sprite.Width / 2), Owner.transform.scale.X, Owner.transform.scale.Y);
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
    }
    public class ResetListener : RenderComponent
    {
        public ResetListener(int prio, MonoEntity owner) : base(prio, owner, null)
        {

        }
        public override void Draw(Graphics g)
        {
            g.DrawString("FEELSBADMAN", new Font(FontFamily.GenericSerif, 50), new SolidBrush(Color.Red), new Point((int)Owner.transform.position.X, (int)Owner.transform.position.Y));
        }
        public void Reset()
        {
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
    }
}
