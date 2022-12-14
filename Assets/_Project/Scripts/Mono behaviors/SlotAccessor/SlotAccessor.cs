using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class SlotAccessor : MonoBehaviour//, IEquatable<Item>
{
    [Title("Set by inventory")]
    [SerializeField]
    private Slot slot;

    [Title("References")]
    [Tooltip("Image that represents the target to be recognized by clicks")]
    [SerializeField]
    private Image clickableAreaTarget;
    
    [SerializeField]
    private Image disabledOverlapImage;
    
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private TMP_Text amountLabel;

    [SerializeField]
    private Image amountLabelBackground;

    public Item Item => slot.item;
    
    public bool IsInteractable => clickableAreaTarget.raycastTarget;

    public int ItemAmount => slot.amount;
    public bool IsEmpty => slot.item == null;
    
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
    public void Clear()
    {
        slot.item = null;
        slot.amount = 0;
        slot.index = 0;
    }

    [Button]
    public void AddItem (IngredientAsset item, int amount = 1)
        => AddItem((Item)item, amount);

    public void AddItem (Item item, int amount = 1)
        => slot.AddItem(item, amount);

    public void ChangeSlots (SlotAccessor otherSlot)
    {
        if (this == otherSlot)
            return;

        var resultSlot = otherSlot.slot.ChangeSlots(slot);
        slot.ChangeSlots(resultSlot);

        UpdateHUD();
        otherSlot.UpdateHUD();
    }

    [Button]
    public void UpdateHUD()
    {
        if (slot?.item?.Icon != null)
        {
            backgroundImage.enabled = true;
            backgroundImage.sprite = slot.item.Icon;
        }
        else
            backgroundImage.enabled = false;

        amountLabel.text = slot?.item
            ? slot.amount.ToString() : "";
        // amountLabelBackground.enabled = slot?.item;
        disabledOverlapImage.enabled = !IsInteractable;
    }

    public void UpdateSlotAlpha (float newAlpha)
    {
        backgroundImage.SetAlpha(newAlpha);
        amountLabelBackground.SetAlpha(newAlpha);
        amountLabel.SetAlpha(newAlpha);
    }

    public bool Equals (Item other)
    {
        if (ReferenceEquals(slot.item, other))
            return true;
        if (ReferenceEquals(slot.item, null))
            return false;
        if (ReferenceEquals(other, null))
            return false;
        
        return slot.Equals(other);
    }

    public bool IsIngredient()
        => slot.item != null && slot.item is IngredientAsset;

    public void ReduceItem()
        => slot.ReduceItem();
}