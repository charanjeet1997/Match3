using UnityEngine;
namespace ModulerUISystem
{
    [ExecuteInEditMode]
    public class TextFitter : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public RectTransform rect;
        public float margin = 0f;
   
        void Update()
        {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, text.GetPreferredValues().x+margin);
            Debug.Log("TextFitter ");
        }
    }
}
