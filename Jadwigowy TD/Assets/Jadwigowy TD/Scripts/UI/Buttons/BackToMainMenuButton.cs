using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class BackToMainMenuButton : MonoBehaviour {

        public void BackToMainMenu() {
            SceneManager.LoadScene(0);
        }
    }
}
