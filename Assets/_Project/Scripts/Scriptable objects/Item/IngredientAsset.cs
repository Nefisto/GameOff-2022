using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Game off/Ingredient", order = 0)]
public class IngredientAsset : Item
{
    public bool Equals (IngredientAsset other)
        => name == other.name;
}