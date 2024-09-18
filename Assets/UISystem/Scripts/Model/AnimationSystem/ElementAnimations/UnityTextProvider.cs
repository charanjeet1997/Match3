namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnityTextProvider : TextProvider
    {
        public Text unityText;

        public override void OnTextUpdated(string text)
        {
            base.OnTextUpdated(text);
            this.unityText.text = text;
        }
    }
}