using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private int maxPasses = 5;
    private int passes;

    private float money = 100f;
    public const string currency = "BTC";
    public float Money { get { return money; } set { money = value; } }
    private int score;// TODO score awarding

    [SerializeField] private Canvas gameOverCanvas;

    private float difficultyLevel;
    [SerializeField] private float difficultyIncrease = 5f, difficultyRange = 100f;
    [SerializeField] private GameObject[] enemiesPrefabs;
    [SerializeField] private EnemySpawner spawner;
    private bool isWaveInProgress, spawned;

    private const float
        SpawnCooldownInSeconds = 1f,
        StartWaveDelayInSeconds = 5f,
        WaveDelayInSeconds = 3f;

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
        gameOverCanvas.enabled = false;
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
        GameObject enemyPrafab = null;

        int i = 1;
        while (i <= enemiesPrefabs.Length && enemyPrafab == null) {
            if (enemies <= i * difficultyRange)
                enemyPrafab = enemiesPrefabs[i - 1];
            i++;
        }

        for (i = 0; i < enemies; i++) {
            spawner.Spawn(enemyPrafab);
            yield return new WaitForSeconds(SpawnCooldownInSeconds);
        }
        spawned = true;
    }

    private void Update() {
        if (isPlaying) {
            if (!isWaveInProgress) {
                difficultyLevel += difficultyIncrease;
                StartCoroutine(SpawnWaveWithDelay((int)difficultyLevel, WaveDelayInSeconds));
            }
            if (isWaveInProgress && spawned && !AreEnemiesAlive)
                isWaveInProgress = false;
        }
    }

    public void Pass() {
        passes++;
        if (passes >= maxPasses)
            Lose();
    }

    public void Lose() {
        // TODO ending
        isPlaying = false;
        Debug.Log("game over");
        Time.timeScale = 0;
        gameOverCanvas.enabled = true;
    }

    private void OnDestroy() {
        Time.timeScale = 1;
    }

    [SerializeField] private Text moneyText, scoreText;
    private void OnGUI() {
        moneyText.text = Money + " " + currency;
        scoreText.text = score.ToString();
    }
}
