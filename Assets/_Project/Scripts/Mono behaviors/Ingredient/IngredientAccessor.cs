using Sirenix.OdinInspector;
using UnityEngine;

public class IngredientAccessor : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField]
    public IngredientAsset ingredient;

    public string Name => ingredient != null ? ingredient.name : "empty";

    public void CollectIngredient()
        => Destroy(gameObject);
}