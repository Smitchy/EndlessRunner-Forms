using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMD2Project___endless_running
{
    public interface IComponent
    {

        int Priority { get;}

        void FixedUpdate();
    }


    public class MockComponent : IComponent
    {
        private int priority;
        public int Priority { get => priority;}

        public MockComponent(int prio)
        {
            priority = prio;
        }

        public void FixedUpdate()
        {
            Console.WriteLine("I have prio = " + priority);
        }
    }
}
