using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else   
            Application.Quit();
        #endif
    }

    public void ChangeSceneToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void ChangeSceneToGameplay()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void ChangeSceneToLogout()
    {
        SceneManager.LoadScene("LogoutScene");
    }
}
