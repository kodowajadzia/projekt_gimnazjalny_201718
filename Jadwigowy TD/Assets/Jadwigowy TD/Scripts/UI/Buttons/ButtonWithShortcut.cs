using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithShortcut : Button {

    public KeyCode shortcut;
	
	void Update () {
        if (Input.GetKeyDown(shortcut))
            onClick.Invoke();
	}
}
