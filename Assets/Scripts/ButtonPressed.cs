using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonPressed : MonoBehaviour
{
    // Start is called before the first frame update

    public Button _button;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.FindKeyOnCurrentKeyboardLayout("f").wasPressedThisFrame) {
            _button.onClick.Invoke();
        }
        
    }
}
