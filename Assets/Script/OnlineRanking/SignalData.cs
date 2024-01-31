using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public struct SignalData
{
    public string order;
    public string message;
    public string value;
    public string valueType;
}

public struct SignalLabel
{
    public const string Request = "Request";
    public const string Update = "Update";
}

public struct ValueType
{
    public const string Text = "text";
    public const string Number = "number";
    public const string JSON = "json";
}

public struct SignalDataElem
{
    public SignalData request;


    public SignalDataElem(string order = "", string message = "", string value = "", string valueType = "")
    {
        request.order = order;
        request.message = message;
        request.value = value;
        request.valueType = valueType;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(request);
    }
}
