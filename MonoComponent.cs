using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    public abstract class MonoComponent
    {
        private MonoEntity owner;
        public MonoEntity Owner { get => owner; }
        private int priority;
        public int Priority { get => priority; }

        public virtual void FixedUpdate()
        {
            return;
        }
        public virtual void Update()
        {
            return;
        }
        protected MonoComponent(int prio, MonoEntity owner)
        {
            this.owner = owner;
            this.priority = prio;
        }
    }


    public class MockComponent : MonoComponent
    {
        public MockComponent(int prio, MonoEntity owner) : base( prio, owner)
        {

        }

        public override void FixedUpdate()
        {
 
        }
        public override void Update()
        {
            if (Input.OnKeyPressed(Keys.Space))
            {
                
            }
            if (Input.OnKeyHeld(Keys.W))
            {                
                Owner.transform.position.Y -= 5;
                Owner.transform.rotation = 270;
            }
            if (Input.OnKeyHeld(Keys.A))
            {
                Owner.transform.position.X -= 5;
                Owner.transform.rotation = 180;
            }
            if (Input.OnKeyHeld(Keys.S))
            {
                Owner.transform.position.Y += 5;
                Owner.transform.rotation = 90;
            }
            if (Input.OnKeyHeld(Keys.D))
            {
                Owner.transform.position.X += 5;
                Owner.transform.rotation = 0;
            }
        }
    }
}
