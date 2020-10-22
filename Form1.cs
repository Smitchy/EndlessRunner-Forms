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
                        if (comp.Owner.isActive)
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

            for (int i = 0; i < comps.Keys.Count; i++)
            {
                KeyValuePair<int, List<MonoComponent>> kvP = comps.ElementAt(i);

                for (int v = 0; v < kvP.Value.Count; v++)
                {
                    if (v < 0)
                        break;

                    MonoComponent comp = kvP.Value[v];

                    if (comp.Owner.isActive)
                        comp.Update();
                    if (comp.Owner.isDestroyed)
                    {
                        comp.Owner.RemoveComponent(comp);
                        v--;
                    }
                }
                if (kvP.Value.Count == 0)
                    i--;
            }
            Time.lastUpdate = DateTime.Now.TimeOfDay;
        }

        private Bitmap bitmap;
        Graphics graphics;
        bool blankScreen;
        private int counter = 0;
        private void Render()
        {
            if (renderComps.Count > 0)
            {

                Bitmap temp = new Bitmap(canvas.Width, canvas.Height);
                graphics = Graphics.FromImage(temp);

                for (int i = 0; i < renderComps.Keys.Count; i++)
                {
                    KeyValuePair<int, List<RenderComponent>> kvP = renderComps.ElementAt(i);

                    for (int v = 0; v < kvP.Value.Count; v++)
                    {
                        if (v < 0)
                            break;

                        RenderComponent rc = kvP.Value[v];

                        if (rc.Owner.isActive)
                        {
                            rc.Draw(graphics);


                        }
                        if (rc.Owner.isDestroyed)
                        {
                            rc.Owner.RemoveComponent(rc);
                            v--;
                        }
                    }
                    if (kvP.Value.Count == 0)
                        i--;
                }
                blankScreen = false;
                canvas.Image = temp;
                bitmap.Dispose();
                bitmap = temp;
            }
            else if (!blankScreen)
            {
                Bitmap temp = new Bitmap(canvas.Width, canvas.Height);
                graphics = Graphics.FromImage(temp);
                canvas.Image = temp;
                bitmap.Dispose();
                bitmap = temp;
                blankScreen = true;
            }
            if (counter == 100)
            {
                GC.Collect();
                counter = 0;
            }
            else
                counter++;

            Application.DoEvents();

        }
    }
    public static class Time
    {
        public static TimeSpan lastUpdate;
        public static TimeSpan currentTime;
        public static float deltaTime => (currentTime - lastUpdate).Milliseconds;

    }
}
