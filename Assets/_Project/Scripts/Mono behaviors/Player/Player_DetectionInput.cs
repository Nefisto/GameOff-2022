using System.Collections.Generic;
using System.Linq;
using NTools;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Player
{
    private void RegisterCollectInput()
    {
        GameInputAccessor.Gameplay.Collect.performed += OnPerformCollect;
    }

    private void OnPerformCollect (InputAction.CallbackContext _)
    {
        // var foundIngredients = new List<Collider2D>();
        var foundIngredients = Physics2D.OverlapCircleAll(transform.position, collectRadius, LayerMask.GetMask("Ingredient"));//collectFilter, foundIngredients);

        // if (foundIngredients.Count == 0)
        if (foundIngredients.Length == 0)
            return;

        var playerPosition = transform.position;
        var nearestIngredient = foundIngredients
            .OrderBy(c2 => Vector2.Distance(c2.transform.position, playerPosition))
            .First()
            .GetComponent<IngredientAccessor>();
        
        if (inventory.TryAddItem(nearestIngredient.ingredient))
        {
            nearestIngredient.CollectIngredient();
            
            inventory.UpdateHUD();
            
            EventHandler.RaiseEvent(GameEventsNames.SUCCESSFULLY_COLLECT_FLOWER);
        }
    }
}