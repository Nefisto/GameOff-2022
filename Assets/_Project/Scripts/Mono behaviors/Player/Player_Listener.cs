using NTools;

public partial class Player
{
    private void RegisterListeners()
    {
        EventHandler.RegisterEvent(GameEventsNames.OPEN_CRAFT_HUD, OnOpenCraftMenu);
        EventHandler.RegisterEvent(GameEventsNames.CLOSE_CRAFT_HUD, OnCloseCraftMenu);
    }

    private void OnCloseCraftMenu()
    {
        GameInputAccessor.Gameplay.Movement.Enable();
        GameInputAccessor.Gameplay.Collect.Enable();
    }

    private void OnOpenCraftMenu()
    {
        GameInputAccessor.Gameplay.Movement.Disable();
        GameInputAccessor.Gameplay.Collect.Disable();
    }
}