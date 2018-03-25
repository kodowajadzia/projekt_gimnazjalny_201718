using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class StartEndlessModeButton : MonoBehaviour {

        public void StartMode() {
            SceneManager.LoadScene("Scenes/Endless Mode");
        }
    }
}
