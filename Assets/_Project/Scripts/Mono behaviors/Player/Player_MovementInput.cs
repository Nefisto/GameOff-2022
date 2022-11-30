using UnityEngine;

public partial class Player
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    public bool CanMove()
        => currentDirection != Vector2.zero;

    public void Move (Vector2 direction)
    {
        // transform.Translate(direction * speed * Time.deltaTime);
        var moveDirection = direction * (speed * Time.deltaTime);
        rigidbody2D.MovePosition((Vector2)transform.position + moveDirection);
        
        SetAnimatorMovement(direction);
        animator.Play("Player walk");
    }

    private void SetAnimatorMovement (Vector2 direction)
    {
        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);
    }

    private void RegisterMovementInput()
    {
        GameInputAccessor.Gameplay.Movement.performed += ctx => currentDirection = ctx.ReadValue<Vector2>();
        GameInputAccessor.Gameplay.Movement.canceled += _ =>
        {
            animator.Play("Player idle");
            currentDirection = Vector2.zero;
        };
    }
}