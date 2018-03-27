using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class ExitButton : MonoBehaviour {

        public void Exit() {
            Application.Quit();
            Debug.Log("exit");
        }
    }
}
