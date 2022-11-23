using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Game off/Recipe", order = 0)]
public class Recipe : ScriptableObject, IEnumerable<IngredientAsset>
{
    [Title("Ingredients")]
    public IngredientAsset ingredientA;
    public IngredientAsset ingredientB;
    public IngredientAsset ingredientC;
    
    public PotionAsset result;

    public bool Contains (IngredientAsset ingredientAsset)
        => this.Any(asset => asset.Equals(ingredientAsset));

    public IEnumerator<IngredientAsset> GetEnumerator()
    {
        yield return ingredientA;
        yield return ingredientB;
        yield return ingredientC;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}