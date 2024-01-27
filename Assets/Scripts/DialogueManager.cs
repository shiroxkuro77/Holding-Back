using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    private static DialogueManager instance;

    public bool dialogueIsPlaying { get; private set;}

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Found more than one Dialogue Manager");
        }
        instance = this;
    }

    public static DialogueManager GetInstance() {
        return instance;
    }

    private void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update() {
        if (!dialogueIsPlaying) {
            return;
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame) {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory() {
        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
        } else {
            ExitDialogueMode();
        }
    }
}
