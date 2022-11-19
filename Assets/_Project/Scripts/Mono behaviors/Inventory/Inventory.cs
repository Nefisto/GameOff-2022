using System;
using System.Collections;
using System.Linq;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;

public partial class Inventory : LazyMonoBehaviour
{
    [Title("References")]
    [SerializeField]
    private Transform slotFolder;
    
    [Title("Debug")]
    [ReadOnly]
    [SerializeField]
    private int size;
    
    [DisableInEditorMode]
    [ReadOnly]
    [SerializeField]
    private SlotAccessor[] items;
    
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

        yield return new WaitForSeconds(1f);
        
        UncollapseInventory();
    }

    [Button]
    public bool TryAddItem(Item item)
    {
        var hasStacked = TryToStackItem(item);

        return hasStacked || TryToAddItemInAnEmptySlot(item);
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
            slotAccessor.Clear();
    }
}