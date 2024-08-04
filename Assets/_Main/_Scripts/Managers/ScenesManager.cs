using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    private void Awake()
    {
        if (ScenesManager.instance) Destroy(this);
        else ScenesManager.instance = this;

        DontDestroyOnLoad(this);
    }

    public void GoToGameScene() => GoToScene("Game");
    public void GoToMainScene() => GoToScene("Main");

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
