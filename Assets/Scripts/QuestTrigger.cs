using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
    
public class QuestTrigger : MonoBehaviour
{

    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool playerInRange;

    private Story currentStory;

    private void Awake() {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update() {

        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            return;
        }

        if (playerInRange) {
            visualCue.SetActive(true);
            
            if (Keyboard.current.FindKeyOnCurrentKeyboardLayout("f").wasPressedThisFrame) {
                StartCoroutine(WaitAndTriggerAction());
            }
        } else {
            visualCue.SetActive(false);
        }

        if (currentStory == null) {
            return;
        }
        
        /*
        int catsaved = int.Parse(currentStory.variablesState["catsaved"].ToString());

        if (catsaved == 1) {
            GameObject parentObject = transform.parent.gameObject;
            parentObject.SetActive(false);
        }*/

    }

    IEnumerator WaitAndTriggerAction()
    {

        // Wait for 1 seconds
        yield return new WaitForSeconds(0.1f);

        currentStory = new Story(inkJSON.text);
        DialogueManager.GetInstance().EnterDialogueMode(currentStory);
       // playercontrols.enableMovement();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;            
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;            
        }
    }


}
