using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public static class GameInputAccessor
{
    private static GameInput _gameInput;

    private static GameInput GameInput
    {
        get
        {
            if (_gameInput != null)
                return _gameInput;

            _gameInput = new GameInput();
            _gameInput.Enable();

            return _gameInput;
        }
    }

    public static InputActionAsset asset => GameInput.asset;

    public static void Dispose()
        => GameInput.Dispose();

    public static InputBinding? bindingMask
    {
        get => GameInput.bindingMask;
        set => GameInput.bindingMask = value;
    }

    public static ReadOnlyArray<InputDevice>? devices
    {
        get => GameInput.devices;
        set => GameInput.devices = value;
    }

    public static ReadOnlyArray<InputControlScheme> controlSchemes => GameInput.controlSchemes;

    public static bool Contains (InputAction action)
        => GameInput.Contains(action);

    public static IEnumerator<InputAction> GetEnumerator()
        => GameInput.GetEnumerator();

    public static void Enable()
        => GameInput.Enable();

    public static void Disable()
        => GameInput.Disable();

    public static IEnumerable<InputBinding> bindings => GameInput.bindings;

    public static InputAction FindAction (string actionNameOrId, bool throwIfNotFound = false)
        => GameInput.FindAction(actionNameOrId, throwIfNotFound);

    public static int FindBinding (InputBinding bindingMask, out InputAction action)
        => GameInput.FindBinding(bindingMask, out action);

    public static GameInput.GameplayActions Gameplay => GameInput.Gameplay;

    public static InputControlScheme KeyboardmouseScheme => GameInput.KeyboardmouseScheme;
}