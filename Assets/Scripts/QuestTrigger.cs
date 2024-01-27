using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
    
public class QuestTrigger : MonoBehaviour
{

    [SerializeField] private GameObject visualCue;
    private bool playerInRange;


    private void Awake() {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update() {
        if (playerInRange) {
            visualCue.SetActive(true);
        } else {
            visualCue.SetActive(false);
        }
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
