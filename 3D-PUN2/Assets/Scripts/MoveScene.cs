using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void OnSceneMoved(string sceneName)
    {
        FadeManager.Instance.LoadScene(sceneName, 1f);
    }
}
