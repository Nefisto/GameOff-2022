using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Game off/Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [Title("Ingredients")]
    [SerializeField]
    private IngredientAsset ingredientA;

    [SerializeField]
    private IngredientAsset ingredientB;

    [SerializeField]
    private IngredientAsset ingredientC;

    [SerializeField]
    private PotionAsset result;
}