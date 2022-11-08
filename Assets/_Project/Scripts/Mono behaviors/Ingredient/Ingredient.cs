using Sirenix.OdinInspector;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField]
    private IngredientAsset ingredient;

    public string Name => ingredient != null ? ingredient.name : "empty";
}