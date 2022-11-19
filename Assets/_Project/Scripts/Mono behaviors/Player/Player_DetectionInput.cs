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
        var foundIngredients = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, collectRadius, collectFilter, foundIngredients);

        if (foundIngredients.Count == 0)
            return;

        var playerPosition = transform.position;
        var nearestIngredient = foundIngredients
            .OrderBy(c2 => Vector2.Distance(c2.transform.position, playerPosition))
            .First()
            .GetComponent<IngredientAccessor>();
        
        if (inventory.TryAddItem(nearestIngredient.ingredient))
            nearestIngredient.CollectIngredient();
    }
}