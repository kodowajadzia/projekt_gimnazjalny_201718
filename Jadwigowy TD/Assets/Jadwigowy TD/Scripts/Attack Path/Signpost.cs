using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
public class Signpost : MonoBehaviour {

    public const string StartSignpostTag = "Start Signpost";

    [SerializeField] private Signpost nextSignpost;
    public Signpost NextSignpost { get { return nextSignpost; } }

    public enum Type {
        Start,
        Medial,
        End,
    }

    public Type SingpostType {
        get {
            if (nextSignpost == null)
                return Type.End;
            else if (tag == StartSignpostTag)
                return Type.Start;
            else
                return Type.Medial;
        }
    }
    

#if UNITY_EDITOR
    [SerializeField] private bool showPath = true;

    private readonly Vector3 offset = new Vector3(0, 1);

    void Update() {
        if (showPath) {
            if (nextSignpost != null)
                Debug.DrawLine(transform.position, nextSignpost.transform.position, Color.red);
        }
    }

    private void OnDrawGizmos() {
        if (tag == StartSignpostTag)
            Handles.Label(transform.position + offset, "Start");

        if (SingpostType == Type.End)
            Handles.Label(transform.position + offset, "End");
    }
#endif
}
