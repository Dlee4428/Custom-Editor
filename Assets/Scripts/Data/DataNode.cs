using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataNode
{
    [SerializeField] private string name;
    public string Name { get { return name; } set { name = value; } }
}
