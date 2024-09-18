using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModulerUISystem
{
    public class ProgressBar : MonoBehaviour
    {
        [Range(0, 1), SerializeField] private float progress;
        private float currentPorgress = 0;
        [SerializeField] private ProgressEvent onProgressChange;
        [SerializeField] private Image progressImage;
        private TMP_Text progressLabel;
        private Image progressHolderImage;
        

        public float Progress
        {
            get => progress;
            set
            {
                
                progress = value;
                progressImage.fillAmount = progress;
                progressLabel.text = (progress * 100f).ToString();
            } 
        }
        

        public TMP_Text ProgressLabel
        {
            get => progressLabel;
            private set => progressLabel = value;
        }

        public Image ProgressImage
        {
            get => progressImage;
            set => progressImage = value;
        }

        [Serializable]
        partial class ProgressEvent : UnityEvent<float>
        {
        }

        private void OnProgressChangedInvoke(float progress)
        {
            onProgressChange?.Invoke(progress);
        }

        public void Init()
        {
            Transform child = transform.GetChild(0);
            progressLabel = child.GetComponentInChildren<TMP_Text>();
            progressHolderImage = child.GetComponentInChildren<Image>();
        }

        private void OnValidate()
        {
            Init();
            Progress = progress;
        }
    }
}