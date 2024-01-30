using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public struct Request
{
    public string order;
    public string message;
}

public struct RequestLabel
{
    public static string Request = "Request";
    public static string Update = "Update";
}

public struct RequestServer
{
    public Request request;


    public RequestServer(string order = "", string message = "")
    {
        request.order = order;
        request.message = message;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(request);
    }
}
