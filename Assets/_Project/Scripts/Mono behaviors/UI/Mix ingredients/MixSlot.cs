using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MixSlot : MonoBehaviour
{
    [Title("References")]
    [SerializeField]
    private Image backgroundImage;

    public SlotAccessor owner;

    public string Name => owner != null ? owner.Item.name : "Empty";
    public bool IsEmpty => owner == null;

    public void AddItem(SlotAccessor sa)
    {
        owner = sa;
        UpdateHUD(owner.Item.Icon);
    }

    public void RemoveItem()
    {
        owner = null;
        UpdateHUD(null);
    }
    
    private void UpdateHUD(Sprite icon)
        => backgroundImage.sprite = icon;
}