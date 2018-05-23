using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class StartGameButton : MonoBehaviour {

        public void StartGame() {
            SceneManager.LoadScene(1);
        }
    }
}
