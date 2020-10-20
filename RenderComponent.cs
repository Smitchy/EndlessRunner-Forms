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
            if(rotation != Owner.transform.rotation)
            {
                var rotDif = Owner.transform.rotation - rotation;
                switch (rotDif)
                {
                    case 180:
                    case -180:
                        if(rotation == 90 || rotation == 270)
                        {
                            sprite.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        }
                            //sprite.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        else if (rotation == 0 || rotation == 180)
                        {
                            sprite.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        break;
                    case 270:
                    case -90:
                        sprite.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case 90:
                    case -270:
                        sprite.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                }
                rotation = Owner.transform.rotation;
            }
            g.DrawImage(sprite, Owner.transform.position.X, Owner.transform.position.Y, Owner.transform.scale.X,Owner.transform.scale.Y);
        }

    }
}
