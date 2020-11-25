using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMD2Project___endless_running
{
    class RenderComponent : MonoComponent
    {
        public Image sprite { get; set; }
        public float rotation = -1f;

        public RenderComponent(int prio, MonoEntity owner, Image s) : base(prio, owner)
        {
            sprite = s;

        }
        public void Draw(Graphics g)
        {
           
            /*if(rotation != Owner.transform.rotation)
            {
                var rotDif = Owner.transform.rotation - rotation;
                switch (rotDif)
                {
                    case 180:
                    case -180:
                        if (rotation == 0 || rotation == 180)
                        {
                            sprite.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        break;
                }
                rotation = Owner.transform.rotation;
            }*/
            g.DrawImage(sprite, Owner.transform.position.X - (sprite.Width/2), Owner.transform.position.Y - (sprite.Width / 2), Owner.transform.scale.X, Owner.transform.scale.Y);
        }

        public override void FixedUpdate()
        {
           
        }

        public override void Update()
        {
           
        }
    }
}
