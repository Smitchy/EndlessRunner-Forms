using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GMD2Project___endless_running
{
    public class CircleCollider : MonoComponent
    {
        public float radius;
        public List<int> colWithLayers;
        public delegate void OnCollision(CircleCollider other);
        public OnCollision OnCollisionEvent;

        public CircleCollider(int prio, MonoEntity owner, float radius, List<int> colWithLayers) : base(prio, owner)
        { 
            this.radius = radius;
            this.colWithLayers = colWithLayers;  
        }

        public void CollisionCheck(List<CircleCollider> collidersToCheck)
        {
            foreach (var circleCollider in collidersToCheck)
            {
                float dist = CalculateDistance(this.Owner.transform.position, circleCollider.Owner.transform.position);

                if(dist < circleCollider.radius + this.radius)
                {
                    OnCollisionEvent.Invoke(circleCollider);
                }
            }
        }

        private float CalculateDistance(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
        }
    }
}
