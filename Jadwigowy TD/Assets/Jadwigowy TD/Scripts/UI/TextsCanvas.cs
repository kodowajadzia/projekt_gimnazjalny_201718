using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextsCanvas : MonoBehaviour {

    public Canvas canvas;
    public KeyCode shortcut = KeyCode.Space;
	
	void Update () {
        if (Input.GetKeyDown(shortcut))
            canvas.enabled = !canvas.enabled;
	}
}
