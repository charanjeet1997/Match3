using UnityEngine;

namespace ToolSystem
{
    public class Tool
    {
        private bool active;
        private string name;
        private Sprite toolIcon;

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                OnToolStatusChanged();
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        public Tool(bool active, string name, Sprite toolIcon)
        {
            this.active = active;
            this.name = name;
            this.toolIcon = toolIcon;
        }
        public virtual bool IsReadyToRun()
        {
            return false;
        }
        public virtual Tool OnToolRunning()
        {
            return this;
        }

        public virtual void OnToolStatusChanged()
        {
            
        }

        public virtual void OnDestroy()
        {
            
        }
        
        
        
    }
}