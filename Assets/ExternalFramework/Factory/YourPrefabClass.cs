using System;
using System.Collections;
using System.Collections.Generic;
using Game.Factories;
using UnityEngine;

public class YourPrefabClass : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    public Transform pivot
    {
        get { return _pivot;}
    }
}
