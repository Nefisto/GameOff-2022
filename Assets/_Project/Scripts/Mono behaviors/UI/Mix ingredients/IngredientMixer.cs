using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTools;
using Sirenix.OdinInspector;
using UnityEngine;

public class IngredientMixer : MonoBehaviour, IEnumerable<MixSlot>
{
    [Title("Settings")]
    [SerializeField]
    private List<Recipe> possibleRecipes;
    
    [Title("References")]
    [SerializeField]
    private MixSlot slotA;

    [SerializeField]
    private MixSlot slotB;

    [SerializeField]
    private MixSlot slotC;
    
    public void Brew()
    {
        if (!CheckIfSlotsAreaFilledCorrectly())
        {
            EventHandler.RaiseEvent(GameEventsNames.FAILED_TO_BREW);
            
            return;
        }

        var validRecipe = FindForValidRecipes();

        if (validRecipe == null)
        {
            EventHandler.RaiseEvent(GameEventsNames.FAILED_TO_BREW);

            return;
        }

        Debug.Log($"Success");
    }

    private Recipe FindForValidRecipes()
    {
        var validRecipe = possibleRecipes
            .FirstOrDefault(r => r.Contains(slotA.owner.Item)
                                 && r.Contains(slotB.owner.Item)
                                 && r.Contains(slotC.owner.Item));

        return validRecipe;
    }

    private bool CheckIfSlotsAreaFilledCorrectly()
        => !slotA.IsEmpty
           && !slotB.IsEmpty
           && !slotC.IsEmpty;

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