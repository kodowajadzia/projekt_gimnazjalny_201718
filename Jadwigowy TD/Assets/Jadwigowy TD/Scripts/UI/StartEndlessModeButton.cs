using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndlessModeButton : MonoBehaviour {

    public void StartMode() {
        SceneManager.LoadScene("Endless Mode");
    }
}
