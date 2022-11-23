using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class IngredientMixer : MonoBehaviour, IEnumerable<MixSlot>
{
    [Title("References")]
    [SerializeField]
    private MixSlot slotA;

    [SerializeField]
    private MixSlot slotB;

    [SerializeField]
    private MixSlot slotC;

    [Button]
    public void ShowItems()
    {
        foreach (var mixSlot in this)
        {
            Debug.Log($"{mixSlot.Name}");
        }
    }
    
    public bool TryAddIngredient(SlotAccessor ingredient, out MixSlot slot)
    {
        foreach (var mixSlot in this)
        {
            if (!mixSlot.IsEmpty)
                continue;
            
            mixSlot.AddItem(ingredient);
            slot = mixSlot;
            return true;
        }

        slot = null;
        return false;
    }

    public IEnumerator<MixSlot> GetEnumerator()
    {
        yield return slotA;
        yield return slotB;
        yield return slotC;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}