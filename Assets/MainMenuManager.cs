using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string firstLevelName = "Level1";
    [SerializeField] private GameObject confirmQuitPopup;

    public void ConfirmQuitGame(){
        confirmQuitPopup.SetActive(true);
    }

    public void CancelQuitGame(){
        confirmQuitPopup.SetActive(false);
    }
    
    public void QuitGame(){
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    public void StartGame(){
        SceneManager.LoadScene(firstLevelName);
    }


}
