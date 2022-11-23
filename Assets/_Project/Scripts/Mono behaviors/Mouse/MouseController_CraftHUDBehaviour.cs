using Sirenix.OdinInspector;
using UnityEngine;

public partial class MouseController
{
    [Title("References")]
    [SerializeField]
    private InventoryViewInCraftHUD inventoryViewInCraftHUD;
    
    private void ClickOnCraftHUD()
    {
        if (!TryGetSlotAccessorThoughtClick(out var sa))
            return;

        inventoryViewInCraftHUD.ToggleIngredientOnMixSlots(sa);
    }
}