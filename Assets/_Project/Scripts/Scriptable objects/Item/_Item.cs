using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public abstract class Item : SerializedScriptableObject
{
    [Tooltip("This will be the icon in inventory/slot")]
    [OdinSerialize]
    public Sprite Icon { get; set; }

    public bool Equals (Item other)
    {
        if (ReferenceEquals(other, null))
            return false;
        
        return name == other.name;
    }
}