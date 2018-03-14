using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private int maxPasses = 5;
    private int passes;
    private float money = 100;
    public float Money { get { return money; } set { money = value; } }

    public void Pass() {
        passes++;
        if (passes > maxPasses)
            Lose();
    }

    private void Lose() {
        Debug.Log("game over");
    }
}
