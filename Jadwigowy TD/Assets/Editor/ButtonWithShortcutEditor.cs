using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonWithShortcut))]
public class ButtonWithShortcutEditor : UnityEditor.UI.ButtonEditor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        ButtonWithShortcut button = (ButtonWithShortcut)target;
        button.shortcut = (KeyCode)EditorGUILayout.EnumPopup("Shortcut", button.shortcut);
    }
}
