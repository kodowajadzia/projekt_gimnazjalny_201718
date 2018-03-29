using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class CanvasManager : MonoBehaviour {

        public Canvas canvas;

        protected void Awake() {
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
            if (canvas)
                canvas.enabled = true;
            else
                Debug.LogWarning("Canvas is not set.");
        }

        public void Hide() {
            if (canvas)
                canvas.enabled = false;
            else
                Debug.LogWarning("Canvas is not set.");
        }
    }
}
