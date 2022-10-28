using UnityEngine;

public class ToBeAnnounced : MonoBehaviour
{
    public Transform folder;
    public GameObject tbaLabel;

    public void CreateLabel()
    {
        var instance = Instantiate(tbaLabel, folder, false);
        ((RectTransform)instance.transform).anchoredPosition = RandomizePosition();
    }

    private Vector2 RandomizePosition() => new(Random.Range(-(1920 * .5f), (1920 * .5f)), Random.Range(-(1080f * .5f), (1080f * .5f)));
}