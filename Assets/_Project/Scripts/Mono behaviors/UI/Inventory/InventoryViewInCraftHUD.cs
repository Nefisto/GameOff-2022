﻿using System.Collections.Generic;
using System.Linq;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;

public class InventoryViewInCraftHUD : MonoBehaviour
{
    public class SlotAndMixSlot
    {
        public SlotAccessor SlotAccessor;
        public MixSlot MixSlot;

        public SlotAndMixSlot(SlotAccessor slotAccessor, MixSlot mixSlot)
        {
            SlotAccessor = slotAccessor;
            MixSlot = mixSlot;
        }
    }
    
    [Title("References")]
    [SerializeField]
    private Inventory actualInventory;
    
    [SerializeField]
    private Transform slotsFolder;

    [SerializeField]
    private SlotAccessor slotAccessorPrefab;

    [SerializeField]
    private IngredientMixer mixer;
    
    [DisableInEditorMode]
    private List<SlotAndMixSlot> itemsView = new();
    
    public SlotAccessor slotAccessor;

    private void Start()
    {
        EventHandler.RegisterEvent(GameEventsNames.OPEN_CRAFT_HUD, OnOpenCraftHud);
    }

    public void ToggleIngredientOnMixSlots (SlotAccessor slot)
    {
        var foundTuple = itemsView
            .First(sms => sms.SlotAccessor == slot);

        if (foundTuple.MixSlot != null)
        {
            foundTuple.MixSlot.RemoveItem();
            foundTuple.MixSlot = null;
            return;
        }

        if (mixer.TryAddIngredient(slot, out var mixSlot))
        {
            foundTuple.MixSlot = mixSlot;
            foundTuple.MixSlot.AddItem(slot);
        }
    }
    
    private void OnOpenCraftHud()
    {
        DestroyChildren();
        ReplicateItemsFromInventory();
    }

    private void ReplicateItemsFromInventory()
    {
        itemsView.Clear();
        foreach (var ingredient in actualInventory.items.Where(x => x.IsIngredient()))
        {
            var instance = Instantiate(ingredient, slotsFolder, true);
            instance.EnableInteraction();
            itemsView.Add(new SlotAndMixSlot(instance, null));
        }

        var slotCount = itemsView.Count;
        while (slotCount < 8)
        {
            var instance = Instantiate(slotAccessorPrefab, slotsFolder);
            instance.UpdateHUD();
            instance.DisableInteraction();
            
            slotCount++;
        }
    }

    private void DestroyChildren()
    {
        foreach (Transform child in slotsFolder)
            Destroy(child.gameObject);
    }

    [Button]
    private void UpdateInventory()
    {
        foreach (var slotAccessor in itemsView)
            slotAccessor.SlotAccessor.UpdateHUD();
    }
}