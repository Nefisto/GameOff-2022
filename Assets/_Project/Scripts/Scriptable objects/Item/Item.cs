using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public abstract class Item : SerializedScriptableObject
{
    [OdinSerialize]
    public Sprite Icon { get; set; }

    public static bool operator ==(Item left, Item right)
    {
        if (ReferenceEquals(null, left))
            return false;
        if (ReferenceEquals(null, right))
            return false;
        if (ReferenceEquals(left, right))
            return true;
        
        return left.name == right.name;
    }

    public static bool operator != (Item left, Item right)
        => !(left == right);
}