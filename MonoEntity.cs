using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;

namespace GMD2Project___endless_running
{
    class MonoEntity
    {
        private List<IComponent> comps;
        public Transform transform;

        public MonoEntity()
        {
            transform = new Transform();
            comps = new List<IComponent>();
        }

        public IComponent AddComponent(IComponent component)
        {
            comps.Add(component);
            Form1.AddComponent(component, component.Priority);
            return component;
        }

        public bool RemoveComponent(IComponent component)
        {
            Form1.RemoveComponent(component);
            return comps.Remove(component);
        }

        public IComponent GetComponent(IComponent c)
        {
            return comps.Where(x => x.GetType() == c.GetType()).First();
        }

        public IEnumerable<IComponent> GetComponents(IComponent c)
        {
            return comps.Where(x => x.GetType() == c.GetType());
        }

        public class Transform
        {
            public Vector2 position;
            public float rotation;
            public Vector2 scale;

            public Transform()
            {
                position = Vector2.Zero;
                rotation = 0;
                scale = Vector2.One;
            }
        }
    }
}
