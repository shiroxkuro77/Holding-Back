using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;

    public TextAsset inkJSON;

    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (!isOccupied){
                    // Assuming the goal is tagged as "Goal" and the player entered the trigger
                    Debug.Log("Player reached the goal!");

                    PlayerControls playercontrols = collision.gameObject.GetComponent<PlayerControls>();
                    playercontrols.disableMovement();

                    if (checkIfClearQuest()) {
                        GameMaster.instance.PlayerWin();
                    } else {
                        GameMaster.instance.PlayerLose();
                    }
                    // uiLevelCleared.enabled = true;
                }
                else{
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                
            }
    }


    //Return true if clear quest, else false
    bool checkIfClearQuest() {

        OldLadyAI[] allComponents = FindObjectsOfType<OldLadyAI>();

        int count = 0;

        foreach (OldLadyAI component in allComponents)
        {
            count++;
        }

        if (count > 0) {
            return false;
        }

        return true;
    }

    public void LoadNextScene()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene index
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there is a next scene
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Log a message if there is no next scene
            Debug.LogError("No next scene available.");
        }
    }
}
