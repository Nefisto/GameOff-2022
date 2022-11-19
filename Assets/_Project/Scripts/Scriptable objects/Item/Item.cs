using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public abstract class Item : SerializedScriptableObject//, IEquatable<Item>
{
    [OdinSerialize]
    public Sprite Icon { get; set; }

    // public bool IsEqualTo (Item other)
    // {
    //     if (ReferenceEquals(other, null))
    //         return false;
    //     
    //     return name == other.name;
    // }

    public bool Equals (Item other)
    {
        if (ReferenceEquals(other, null))
            return false;
        
        return name == other.name;
    }
    
    // public override int GetHashCode()
    //     => base.GetHashCode();
    //
    // public override bool Equals (object obj)
    // {
    //     if (ReferenceEquals(null, obj))
    //         return false;
    //     if (ReferenceEquals(this, obj))
    //         return true;
    //     if (obj.GetType() != GetType())
    //         return false;
    //     return Equals((Item)obj);
    // }
    //
    // public static bool operator == (Item left, Item right)
    // {
    //     if (ReferenceEquals(left, right))
    //         return true;
    //     if (ReferenceEquals(null, left))
    //         return false;
    //     if (ReferenceEquals(null, right))
    //         return false;
    //
    //     return left.name == right.name;
    // }
    //
    // public static bool operator != (Item left, Item right)
    //     => !(left == right);
}