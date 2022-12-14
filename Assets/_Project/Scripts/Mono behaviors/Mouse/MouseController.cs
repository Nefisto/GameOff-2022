using NTools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class MouseController : MonoBehaviour
{
    [Title("References")]
    public GraphicRaycaster raycaster;
    public RectTransform dragItemFolder;
    public Canvas dragCanvas;

    private void Start()
    {
        SetupMachineState();
        
        pointerEventData = new PointerEventData(EventSystem.current);
        
        GameInputAccessor.Gameplay.Click.started += _ => mouseStateMachine.CurrentState.Click();
        GameInputAccessor.Gameplay.Click.canceled += _ => mouseStateMachine.CurrentState.Release();

        EventHandler.RegisterEvent(GameEventsNames.OPEN_CRAFT_HUD, OnOpenCraftHUD);
        EventHandler.RegisterEvent(GameEventsNames.CLOSE_CRAFT_HUD, OnCloseCraftHUD);
    }

    private void OnCloseCraftHUD()
        => mouseStateMachine.ChangeState(gameplayBehaviour);

    private void OnOpenCraftHUD()
        => mouseStateMachine.ChangeState(onCraftBehaviour);
}