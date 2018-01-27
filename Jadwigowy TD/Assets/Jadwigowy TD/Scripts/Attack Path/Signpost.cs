using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Signpost : MonoBehaviour {

    public Signpost nextSignpost;

#if UNITY_EDITOR
    public bool showPath = true;

	void Update () {
        if (showPath) {
            if (nextSignpost != null)
                Debug.DrawLine(transform.position, nextSignpost.transform.position, Color.red);
        }   
	}

    private void OnDrawGizmos() {
        if (tag == "Start Signpost") {
            Vector3 offset = new Vector3(0, 1);
            Handles.Label(transform.position + offset, "Start");
        }
        if(tag == "End Signpost") {
            Vector3 offset = new Vector3(0, 1);
            Handles.Label(transform.position + offset, "End");
        }
    }
#endif

}
