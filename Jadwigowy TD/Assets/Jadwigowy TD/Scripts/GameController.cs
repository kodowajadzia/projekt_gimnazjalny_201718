using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int passes = 5;

    public void Pass() {
        passes--;
        if (passes <= 0)
            Lose();
    }

    private void Lose() {
        Debug.Log("game over");
    }
}
