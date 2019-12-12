using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataBool : DataNode
{
    [SerializeField] private bool value;

    public static explicit operator bool(DataBool data)
    {
        return data.value;
    }
}
