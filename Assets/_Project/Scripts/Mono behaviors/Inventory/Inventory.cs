using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Inventory : MonoBehaviour
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
}