namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Dropdown : PropertyAttribute
    {
        public string lable;
        public string[] items = new string[0];

        public Dropdown(string lable,params string[] items)
        {
            this.lable = lable;
            this.items = items;
        }
    }
}
