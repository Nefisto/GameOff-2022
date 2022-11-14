using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        if (this.item == item)
            this.amount += amount;
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
        owner = other.owner;
    }
    
    private void IncrementAmountIn (int many)
        => amount += many;

    private bool HasSameItemThat (Slot otherSlot)
        => item == otherSlot.item;
}

public class SlotAccessor : MonoBehaviour
{
    [Title("Set by inventory")]
    [HideReferenceObjectPicker]
    [HideLabel]
    [SerializeField]
    private Slot slot;

    [Title("References")]
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private TMP_Text amountLabel;

    [SerializeField]
    private Image amountLabelBackground;
    
    public void Setup (Inventory owner, int index, Item initialItem = null, int itemAmount = 0)
    {
        slot.owner = owner;
        slot.index = index;
        if (initialItem != null)
        {
            slot.item = initialItem;
            slot.amount = itemAmount;
        }
        
        UpdateHUD();
    }
    
    [Button]
    public void AddItem (Item item, int amount = 1)
    {
        slot.AddItem(item, amount);

        UpdateHUD();
    }
    
    public Slot ChangeSlots (Slot otherSlot)
    {
        var resultSlot = slot.ChangeSlots(otherSlot);
        
        UpdateHUD();
        return resultSlot;
    }

    private void UpdateHUD()
    {
        backgroundImage.sprite = slot.item?.Icon;
        amountLabel.text = slot.item 
            ? slot.amount.ToString() : "";
        amountLabelBackground.enabled = slot.item;
    }
}