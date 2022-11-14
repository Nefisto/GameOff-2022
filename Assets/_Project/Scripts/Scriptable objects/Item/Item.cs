using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public abstract class Item : SerializedScriptableObject
{
    [OdinSerialize]
    public Sprite Icon { get; set; }

    protected bool Equals (Item other)
        => this == other;

    public override bool Equals (object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((Item)obj);
    }

    public static bool operator == (Item left, Item right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (ReferenceEquals(null, left))
            return false;
        if (ReferenceEquals(null, right))
            return false;

        return left.name == right.name;
    }

    public static bool operator != (Item left, Item right)
        => !(left == right);
}