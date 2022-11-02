using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField]
    private float speed = 5f;

    [Title("Debug")]
    [ReadOnly]
    [SerializeField]
    private Vector2 currentDirection;
    
    public void Start()
    {
        GameInputAccessor.Gameplay.Movement.performed += ctx => currentDirection = ctx.ReadValue<Vector2>();
        GameInputAccessor.Gameplay.Movement.canceled += _ => currentDirection = Vector2.zero;
    }

    private void Update()
    {
        if (currentDirection != Vector2.zero)
        {
            Move(currentDirection);
        }
    }

    private void Move (Vector2 direction)
    {
        transform.Translate(translation: direction * speed * Time.deltaTime);    
    }
}