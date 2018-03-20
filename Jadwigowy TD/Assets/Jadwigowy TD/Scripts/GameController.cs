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

    private float difficultyLevel = 1;

    [SerializeField] private Text moneyText, scoreText;

    [SerializeField] private EnemySpawner spawner;
    private bool isWaveInProgress, spawned;
    private int wave;
    private const float SpawnCooldownInSeconds = 1, StartWaveDelayInSeconds = 5, WaveDelayInSeconds = 3;
    public bool AreEnemiesAlive {
        get {
            return GameObject.FindGameObjectsWithTag(Enemy.Tag).Length > 0;
        }
    }

    private bool isPlaying;

    private void Awake() {
        spawner = spawner ?? FindObjectOfType<EnemySpawner>();
    }

    private void Start() {
        isPlaying = true;
    }

    public IEnumerator SpawnWaveWithDelay(int enemies, float delayInSeconds) {
        isWaveInProgress = true;
        spawned = false;
        yield return new WaitForSeconds(delayInSeconds);
        StartCoroutine(SpawnWave(enemies));
    }

    public IEnumerator SpawnWave(int enemies) {
        isWaveInProgress = true;
        spawned = false;
        wave++;
        for (int i = 0; i < enemies; i++) {
            spawner.Spawn(EnemyType.FourthClassPS);
            yield return new WaitForSeconds(SpawnCooldownInSeconds);
        }
        spawned = true;
    }

    private void Update() {
        if (isPlaying) {
            if (!isWaveInProgress) {
                difficultyLevel += Mathf.Max(3, 6 - wave);//hardcoded :P
                StartCoroutine(SpawnWaveWithDelay((int)difficultyLevel, WaveDelayInSeconds));
            }
            if (isWaveInProgress && spawned && !AreEnemiesAlive)
                isWaveInProgress = false;
        }
    }

    public void Pass() {
        passes++;
        if (passes > maxPasses)
            Lose();
    }

    public void Lose() {
        isPlaying = false;
        Debug.Log("game over");
    }

    private void OnGUI() {
        moneyText.text = Money + " " + currency;
        scoreText.text = score.ToString();
    }
}
