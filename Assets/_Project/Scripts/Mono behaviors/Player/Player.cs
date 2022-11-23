using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class Player : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField]
    private float speed = 5f;

    [InfoBox("Yellow circle used to represent this value")]
    [SerializeField]
    private float collectRadius = 2f;

    [SerializeField]
    private ContactFilter2D collectFilter;

    [Title("References")]
    [SerializeField]
    private Inventory inventory;

    [Title("Debug")]
    [ReadOnly]
    public Vector2 currentDirection;

    [ReadOnly]
    [ShowInInspector]
    private PlayerStateEnum CurrentState => stateMachine?.CurrentState?.StateEnum ?? PlayerStateEnum.Basic;

    public void Awake()
    {
        RegisterMovementInput();
        RegisterCollectInput();

        RegisterListeners();

        SetupStateMachine();
    }

    private void Update()
        => stateMachine.CurrentState.Update();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.forward, collectRadius);
    }
#endif
}