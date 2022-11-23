using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class MouseController : MonoBehaviour
{
    [Title("References")]
    public GraphicRaycaster raycaster;
    public RectTransform dragItemFolder;
    
    private void Start()
    {
        pointerEventData = new PointerEventData(EventSystem.current);
        
        GameInputAccessor.Gameplay.Click.started += _ => OnPressMouse();
        GameInputAccessor.Gameplay.Click.canceled += _ => OnReleaseMouse();
    }
}