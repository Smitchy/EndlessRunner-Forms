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
    public class MonoEntity
    {
        private List<MonoComponent> comps;
        public Transform transform;
        public bool isActive;
        public bool isDestroyed;

        public string name;

        public MonoEntity(string name)
        {
            transform = new Transform();
            comps = new List<MonoComponent>();
            isActive = true;
            isDestroyed = false;
            this.name = name;
        }

        public MonoComponent AddComponent(MonoComponent component)
        {
            comps.Add(component);
            Form1.AddComponent(component, component.Priority);
            component.Owner = this;
            return component;
        }

        public bool RemoveComponent(MonoComponent component)
        {
            Form1.RemoveComponent(component);
            return comps.Remove(component);
        }

        public MonoComponent GetComponent(MonoComponent c)
        {
            return comps.Where(x => x.GetType() == c.GetType()).First();
        }

        public IEnumerable<MonoComponent> GetComponents(MonoComponent c)
        {
            return comps.Where(x => x.GetType() == c.GetType());
        }

        public void Destroy()
        {
            this.isDestroyed = true;
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
            public Transform(Vector2 position, float rotation, Vector2 scale)
            {
                this.position = position;
                this.rotation = rotation;
                this.scale = scale;
            }
        }
    }
}
