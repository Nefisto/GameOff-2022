using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonOperations : ScriptableObject
{
    public void ChangeScene (int index)
        => SceneManager.LoadScene(index);
}