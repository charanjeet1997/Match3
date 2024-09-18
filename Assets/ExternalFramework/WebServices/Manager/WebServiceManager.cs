
using System;
using System.Collections;
using System.Collections.Generic;
using Games.UnnamedArcade3d;
using UnityEngine;
using ServiceLocatorFramework;
public class WebServiceManager : MonoBehaviour
{
    //public ServerData webServiceData;
    //public ProfileData profileData;
    public static WebServiceManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ServiceLocator.Current.Register<WebServiceManager>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Current.Unregister<WebServiceManager>();
    }
}

