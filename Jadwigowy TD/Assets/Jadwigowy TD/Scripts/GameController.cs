using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private int maxPasses = 5, passes;

    public void Pass() {
        passes++;
        if (passes > maxPasses)
            Lose();
    }

    private void Lose() {
        Debug.Log("game over");
    }
}
