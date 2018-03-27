using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SettingsCanvasManager : MonoBehaviour {

        public Canvas canvas;
        public Text pauseText;
        public bool pause = true;

        void Awake() {
            if (pauseText) {
                if (pause)
                    pauseText.text = "PAUZA";
                else
                    pauseText.text = "";
            } else Debug.LogWarning("PauseText is not set.");

            if (canvas)
                Hide();
            else
                Debug.LogWarning("Canvas is not set.");
        }

        public void ChangeState() {
            if (canvas.enabled)
                Hide();
            else
                Show();
        }

        public void Show() {
            if (canvas) {
                canvas.enabled = true;
                if (pause)
                    Time.timeScale = 0;
            } else
                Debug.LogWarning("Canvas is not set.");
        }

        public void Hide() {
            if (canvas) {
                canvas.enabled = false;
                if (pause)
                    Time.timeScale = 1;
            } else
                Debug.LogWarning("Canvas is not set.");
        }

        public void SaveSettings() {
            // TODO: settings saving
            Debug.Log("\"save\"");
        }
    }
}
