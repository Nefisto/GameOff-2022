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

        Debug.Log($"Found amount {foundIngredients.Count}");
        foundIngredients
            .Select(col => col.GetComponent<Ingredient>())
            .ForEach(i => Debug.Log($"{i.Name}"));
    }
}