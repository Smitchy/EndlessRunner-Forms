using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    public partial class Form1 : Form
    {

        public bool running;

        private TimeSpan lastUpdate = System.DateTime.Now.TimeOfDay;

        private bool spacePressed;

        private TimeSpan fixedUpdateInterval = new TimeSpan(166667);

        private TimeSpan timeForFixedUpdates = new TimeSpan(0);

        private static SortedDictionary<int, List<MonoComponent>> comps = new SortedDictionary<int, List<MonoComponent>>();
        //todo - dictionary for render components
        private static SortedDictionary<int, List<RenderComponent>> renderComps = new SortedDictionary<int, List<RenderComponent>>();

        public Form1()
        {
            InitializeComponent();
        }

        public static void AddComponent(MonoComponent component, int prio)
        {
            if (component.GetType() == typeof(RenderComponent))
            {
                if (!renderComps.ContainsKey(prio))
                {
                    renderComps[prio] = new List<RenderComponent>();
                }
                renderComps[prio].Add((RenderComponent)component);
            }
            else
            {
                if (!comps.ContainsKey(prio))
                {
                    comps[prio] = new List<MonoComponent>();
                }
                comps[prio].Add(component);
            }
        }
        public static void RemoveComponent(MonoComponent component)
        {
            if (component.GetType() == typeof(RenderComponent))
            {
                if (renderComps.ContainsKey(component.Priority))
                {
                    if (renderComps[component.Priority].Contains(component))
                    {
                        renderComps[component.Priority].Remove((RenderComponent)component);
                        if (renderComps[component.Priority].Count == 0)
                        {
                            renderComps.Remove(component.Priority);
                        }
                    }
                }
            }
            else
            {
                if (comps.ContainsKey(component.Priority))
                {
                    if (comps[component.Priority].Contains(component))
                    {
                        comps[component.Priority].Remove(component);
                        if (comps[component.Priority].Count == 0)
                        {
                            comps.Remove(component.Priority);
                        }
                    }
                }
            }
        }

        internal void RunGameLoop()
        {
            running = true;

            while (running)
            {
                HandleInput();
                FixedUpdate();
                Update();
                Render();
            }
        }

        private void HandleInput()
        {
            Input.HandleInput();
        }

        private void FixedUpdate()
        {
            timeForFixedUpdates += DateTime.Now.TimeOfDay - lastUpdate;
            while (timeForFixedUpdates > fixedUpdateInterval)
            {
                foreach (var d in comps.Keys)
                {

                    foreach (MonoComponent comp in comps[d])
                    {
                        if(comp.Owner.isActive)
                            comp.FixedUpdate();
                    }
                }
                timeForFixedUpdates -= fixedUpdateInterval;
            }
            lastUpdate = DateTime.Now.TimeOfDay;
        }

        private void Update()
        {
            Time.currentTime = DateTime.Now.TimeOfDay;
            foreach (var d in comps.Keys)
            {
                foreach (MonoComponent comp in comps[d])
                {
                    if(comp.Owner.isActive)
                        comp.Update();
                }
            }
            Time.lastUpdate = DateTime.Now.TimeOfDay;
        }

        private void Render()
        {
            Bitmap bitmap = new Bitmap(canvas.Width, canvas.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            foreach(var v in renderComps.Keys)
            {
                foreach(RenderComponent rc in renderComps[v])
                {
                    if(rc.Owner.isActive)
                        rc.Draw(graphics);
                }
            }
            canvas.Image = bitmap;
            Application.DoEvents();
        }
    }
    public static class Time
    {
        public static TimeSpan lastUpdate;
        public static TimeSpan currentTime;
        public static float deltaTime = (currentTime - lastUpdate).Ticks;
    }
}
