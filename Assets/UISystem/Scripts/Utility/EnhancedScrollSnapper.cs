using System;
using System.Collections;
using System.Collections.Generic;
// using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Zlib;
using EnhancedUI.EnhancedScroller;
using Games.UnnamedArcade3d.Entities.LittleRed;
using UnityEngine;

public class EnhancedScrollSnapper : MonoBehaviour
{
    [SerializeField] private EnhancedScroller _scroller;
    [SerializeField] private RectTransform referenceRect;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float snapThreshold = 10f;
    private RectTransform container;
    private float referenceHeight;
    private bool isSnapped = false;
    private bool isFirstTime = false;

    private void Start()
    {
        referenceHeight = referenceRect.rect.height;
    }

    private void Update()
    {
        if (container == null)
        {
            container = _scroller.ScrollRect.content;
        }

        if ((Mathf.Abs(container.anchoredPosition.y % referenceHeight) <= snapThreshold) && !isSnapped)
        {
            PlayAudio();
        }

        if (!isSnapped && _scroller.Velocity.magnitude < 10f && !Input.GetMouseButton(0))
        {
            _scroller.Velocity = Vector2.zero;
            container.anchoredPosition = new Vector2(container.anchoredPosition.x,
                Mathf.RoundToInt(container.anchoredPosition.y / referenceHeight) * referenceHeight);
            PlayAudio();
            isSnapped = true;
        }
        else if (_scroller.Velocity.magnitude > 10f || Input.GetMouseButton(0))
        {
            isSnapped = false;
        }
    }

    public void PlayAudio()
    {
        if (!isFirstTime)
        {
            isFirstTime = true;
            return;
        }
        _audioSource.Play();
    }
}