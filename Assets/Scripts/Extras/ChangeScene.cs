using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    public void ChangeSceneToGameplayP1()
    {
        PlayerSelection.PlayerIndex = 1;

        SceneManager.LoadScene("GameplayScene");
        Debug.Log("Player 1 has been set");
    }

    public void ChangeSceneToGameplayP2()
    {
        PlayerSelection.PlayerIndex = 2;
        
        SceneManager.LoadScene("GameplayScene");
        Debug.Log("Player 2 has been set");
    }

    public void ChangeSceneToLogout()
    {
        SceneManager.LoadScene("LogoutScene");
    }
}
