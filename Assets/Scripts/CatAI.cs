using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class CatAI : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            return;
        }


        
    }
}
