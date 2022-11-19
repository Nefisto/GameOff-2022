using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Slot
{
    [ReadOnly]
    public Item item;

    [ReadOnly]
    public int amount;

    [ReadOnly]
    public int index;

    [HideInInspector]
    public Inventory owner;
    
    public void AddItem (Item item, int amount = 1)
    {
        if (this.item != null && this.item.Equals(item))
        {
            this.amount += amount;
        }
        else
        {
            this.item = item;
            this.amount = amount;
        }
    }

    public Slot ChangeSlots (Slot otherSlot)
    {
        if (HasSameItemThat(otherSlot))
        {
            IncrementAmountIn(otherSlot.amount);

            return new Slot();
        }

        var tempSlot = (Slot)MemberwiseClone();
        ShallowCopy(otherSlot);

        return tempSlot;
    }


    private void ShallowCopy (Slot other)
    {
        index = other.index;
        item = other.item;
        amount = other.amount;
    }

    private void IncrementAmountIn (int many)
        => amount += many;

    public bool HasSameItemThat (Slot otherSlot)
        => item.Equals(otherSlot.item);
    // => item == otherSlot.item;

    public bool HasSameItemThat (Item otherItem)
        => item.Equals(otherItem);

    public bool Equals (Item otherItem)
        => item.Equals(otherItem);
    protected bool Equals (Slot other)
        => Equals(item, other.item);
    //
    // public bool Equals (Item other)
    // {
    //     if (ReferenceEquals(item, other))
    //         return true;
    //     if (ReferenceEquals(null, other))
    //         return false;
    //     if (ReferenceEquals(null, item))
    //         return false;
    //     
    //     return item.Equals(other);
    // }
    //
    // public override bool Equals (object obj)
    // {
    //     if (ReferenceEquals(this, obj))
    //         return true;
    //     if (ReferenceEquals(null, obj))
    //         return false;
    //     if (obj.GetType() != this.GetType())
    //         return false;
    //     return Equals((Slot)obj);
    // }
    //
    // public override int GetHashCode()
    //     => (item != null ? item.GetHashCode() : 0);
    //
    // public static bool operator == (Slot left, Slot right)
    //     => Equals(left, right);
    //
    // public static bool operator != (Slot left, Slot right)
    //     => !Equals(left, right);
}