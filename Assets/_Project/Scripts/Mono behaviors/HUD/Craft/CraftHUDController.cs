using NTools;
using Sirenix.OdinInspector;
using UnityEngine;

public class CraftHUDController : MonoBehaviour
{
    [Title("References")]
    [SerializeField]
    private Transform craftFolder;

    private void Start()
    {
        EventHandler.RegisterEvent(GameEventsNames.OPEN_CRAFT_HUD, OpenCraftHUDMenu);

        ((RectTransform)craftFolder).anchoredPosition = Vector2.zero;
    }

    public void CloseCraftHUDMenu()
    {
        craftFolder.gameObject.SetActive(false);
        EventHandler.RaiseEvent(GameEventsNames.CLOSE_CRAFT_HUD);
    }

    private void OpenCraftHUDMenu()
        => craftFolder.gameObject.SetActive(true);
}