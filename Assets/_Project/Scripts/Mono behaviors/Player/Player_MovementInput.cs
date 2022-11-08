using UnityEngine;

public partial class Player
{
    public bool CanMove()
        => currentDirection != Vector2.zero;

    public void Move (Vector2 direction)
        => transform.Translate(direction * speed * Time.deltaTime);

    private void RegisterMovementInput()
    {
        GameInputAccessor.Gameplay.Movement.performed += ctx => currentDirection = ctx.ReadValue<Vector2>();
        GameInputAccessor.Gameplay.Movement.canceled += _ => currentDirection = Vector2.zero;
    }
}