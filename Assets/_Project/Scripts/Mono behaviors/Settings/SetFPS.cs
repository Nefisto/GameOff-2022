using Sirenix.OdinInspector;
using UnityEngine;

public class SetFPS : MonoBehaviour
{
    [Title("Settings")]
    [Tooltip("Set on awake, so you need to reset to apply the changes")]
    [SerializeField]
    private int targetFrameRate = 60;
    
    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}