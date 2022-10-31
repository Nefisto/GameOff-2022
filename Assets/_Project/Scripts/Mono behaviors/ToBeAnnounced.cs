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

    private Vector2 RandomizePosition()
    {
        var halfWidth = Screen.width * .5f;
        var halfHeight = Screen.height * .5f;
        return new Vector2(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight));
    }
}