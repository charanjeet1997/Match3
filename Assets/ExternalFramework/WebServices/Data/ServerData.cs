
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndPointData
{
    public EndPointName endPointName;
    public string endPointUrl;
}
public enum ServerType
{
    Local,
    Live
}
public enum EndPointName
{
    PublishMap,
    GetAllMap
}

[CreateAssetMenu(menuName = "Data/ServerData")]
public class ServerData : ScriptableObject
{
    public string localUrl;
    public string liveUrl;
    [HideInInspector]
    public string currentUrl;
    public ServerType serverType;
    public List<EndPointData> endPointDatas;
    public void OnEnable()
    {
        Validata();
    }
    public void Validata()
    {
        ChangeUrl(serverType);
    }
    public void ChangeUrl(ServerType serverType)
    {
        if (serverType == ServerType.Live)
        {
            currentUrl = liveUrl;
        }
        else
        {
            currentUrl = localUrl;
        }
    }

    public string PrepareUrl(EndPointName endPointName)
    {
        string endpoint = "";
        for (int endpointIndex = 0; endpointIndex < endPointDatas.Count; endpointIndex++)
        {
            if (endPointDatas[endpointIndex].endPointName == endPointName)
            {
                endpoint = endPointDatas[endpointIndex].endPointUrl;
            }
        }

        string url = currentUrl;
        // Debug.Log(url);
        var webURL = currentUrl + endpoint;
        return webURL;
    }
}

