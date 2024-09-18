namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TextProvider : MonoBehaviour
    {
        public string Text
        {
            set { OnTextUpdated(value); }
            get { return text; }
        }

        private string text;

        public virtual void OnTextUpdated(string text)
        {
            this.text = text;
        }
    }
}