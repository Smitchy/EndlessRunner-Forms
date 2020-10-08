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

        public RenderComponent(int prio, MonoEntity owner, Image s) : base(prio, owner)
        {
            sprite = s;

        }
        public void Draw(Graphics g)
        {
            g.DrawImage(sprite, Owner.transform.position.X, Owner.transform.position.Y, Owner.transform.scale.X,Owner.transform.scale.Y);            
        }

    }
}
