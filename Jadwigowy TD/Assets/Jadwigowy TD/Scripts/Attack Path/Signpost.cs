using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Signpost : MonoBehaviour {

    public const string START_SIGNPOST_TAG = "Start Signpost", END_SIGNPOST_TAG = "End Signpost";

    public Signpost nextSignpost;

#if UNITY_EDITOR
    public bool showPath = true;

    private void Start() {
        if (nextSignpost == null && gameObject.tag != END_SIGNPOST_TAG)
            Debug.LogError("If the signpost is the lost one it should has a " + END_SIGNPOST_TAG + " tag.");
    }

    void Update () {
        if (showPath) {
            if (nextSignpost != null)
                Debug.DrawLine(transform.position, nextSignpost.transform.position, Color.red);
        }   
	}

    private void OnDrawGizmos() {
        if (tag == START_SIGNPOST_TAG) {
            Vector3 offset = new Vector3(0, 1);
            Handles.Label(transform.position + offset, "Start");
        }
        if(tag == END_SIGNPOST_TAG) {
            Vector3 offset = new Vector3(0, 1);
            Handles.Label(transform.position + offset, "End");
        }
    }
#endif

}
