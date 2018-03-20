using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private int maxPasses = 5;
    private int passes;

    private float money = 100;
    public float Money { get { return money; } set { money = value; } }

    private int score;//score awarding

    public const string currency = "BTC";

    [SerializeField] private Text moneyText, scoreText;

    public void Pass() {
        passes++;
        if (passes > maxPasses)
            Lose();
    }

    public void Lose() {
        Debug.Log("game over");
    }

    private void OnGUI() {
        moneyText.text = Money + " " + currency;
        scoreText.text = score.ToString();
    }
}
