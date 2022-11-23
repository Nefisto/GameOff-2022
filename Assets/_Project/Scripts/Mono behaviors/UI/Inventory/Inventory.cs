using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using EventHandler = NTools.EventHandler;

public partial class Inventory : LazyMonoBehaviour
{
    [Title("References")]
    [SerializeField]
    private Transform slotFolder;

    [SerializeField]
    private Button collapseButton;
    
    [Title("Debug")]
    [ReadOnly]
    [SerializeField]
    private int size;
    
    [DisableInEditorMode]
    [ReadOnly]
    [SerializeField]
    public SlotAccessor[] items;
    
    private void Awake()
    {
        size = slotFolder.childCount;
        items = new SlotAccessor[size];
        
        for (var i = 0; i < size; i++)
        {
            var slot = slotFolder
                .GetChild(i)
                .gameObject
                .GetComponent<SlotAccessor>();
            
            slot.Setup(this, i);
            items[i] = slot;
        }
    }   

    private IEnumerator Start()
    {
        SetupCollapseValues();
        SetupListeners();

        yield return new WaitForSeconds(1f);
        
        UncollapseInventory();
    }

    private void SetupListeners()
    {
        EventHandler.RegisterEvent(GameEventsNames.OPEN_CRAFT_HUD, OpenCraftHudCallback);
        EventHandler.RegisterEvent(GameEventsNames.CLOSE_CRAFT_HUD, CloseCraftHudCallback);
    }

    private void CloseCraftHudCallback()
    {
        UncollapseInventory();
        EnableCollapseButton();
    }

    private void OpenCraftHudCallback()
    {
        CollapseInventory();
        DisableCollapseButton();
    }

    private void EnableCollapseButton()
        => collapseButton.interactable = true;

    private void DisableCollapseButton()
        => collapseButton.interactable = false;

    [Button]
    public bool TryAddItem(Item item)
    {
        var hasStacked = TryToStackItem(item);

        return hasStacked || TryToAddItemInAnEmptySlot(item);
    }

    public void UpdateHUD()
    {
        foreach (var slotAccessor in items)
        {
            slotAccessor.UpdateHUD();
        }
    }
    
    private bool TryToStackItem (Item item)
    {
        var foundSimilarItem = items
            .FirstOrDefault(x => x.Equals(item));

        if (foundSimilarItem == null)
            return false;
        
        foundSimilarItem.AddItem(item);
        return true;

    }

    private bool TryToAddItemInAnEmptySlot (Item item)
    {
        var foundEmptySpace = items
            .FirstOrDefault(x => x.IsEmpty);

        if (foundEmptySpace == null)
            return false;
        
        foundEmptySpace.AddItem(item);
        return true;
    }

    [Button]
    private void FillSlots()
    {
        size = slotFolder.childCount;
        items = new SlotAccessor[size];
        
        for (var i = 0; i < size; i++)
        {
            var slot = slotFolder
                .GetChild(i)
                .gameObject
                .GetComponent<SlotAccessor>();
            
            items[i] = slot;
        }
    }

    [Button]
    private void Clear()
    {
        foreach (var slotAccessor in items)
        {
            slotAccessor.Clear();
            slotAccessor.UpdateHUD();
        }
    }
}