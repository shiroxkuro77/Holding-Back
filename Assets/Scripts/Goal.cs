using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Goal : MonoBehaviour
{
    public Canvas uiLevelCleared;

    void Start()
	{
		uiLevelCleared.enabled = false;
	}

    void Update() {
        if (uiLevelCleared.enabled && Keyboard.current.enterKey.wasPressedThisFrame) {
            LoadNextScene();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Assuming the goal is tagged as "Goal" and the player entered the trigger
                Debug.Log("Player reached the goal!");

                PlayerControls playercontrols = collision.gameObject.GetComponent<PlayerControls>();
                playercontrols.disableMovement();

                uiLevelCleared.enabled = true;
            }
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
