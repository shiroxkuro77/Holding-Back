using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class EndLevel : MonoBehaviour

{

    [SerializeField] private TextAsset inkJSON;

    bool enable = false;

    void Update() {
        if (!enable) {
            enable = true;
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }

        if (enable && !DialogueManager.GetInstance().dialogueIsPlaying) {
            GameMaster.instance.PlayerWin();
        }

    }
}
