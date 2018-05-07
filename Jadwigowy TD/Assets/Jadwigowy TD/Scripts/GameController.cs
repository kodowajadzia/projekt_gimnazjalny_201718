using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public uint maxPasses = 5;
    public uint Passes { get; private set; }

    public const string Currency = "bitcoin";
    public float moneyAtStart = 10;
    public float money;
    private int score;

    public Canvas gameOverCanvas;

    private float difficultyLevel;
    public float difficultyIncrease = 5f, difficultyRange = 100f;
    public float spawnCooldownInSeconds = 1f, waveDelayInSeconds = 3f;
    public GameObject[] enemiesPrefabs = new GameObject[0];
    public Spawner spawner;
    private bool isWaveInProgress, spawned;
    public bool isPlaying;
    public Text moneyText, scoreText, livesText;

    public bool AreEnemiesAlive {
        get { return FindObjectsOfType<Enemy>().Length > 0; }
    }

    private void Awake() {
        money = moneyAtStart;
    }

    private void Start() {
        if (gameOverCanvas)
            gameOverCanvas.enabled = false;
        else
            Debug.LogWarning("Game-over-canvas is not set.");
        isPlaying = true;
    }

    public IEnumerator SpawnWaveWithDelay(int enemies, GameObject enemyPrefab, float delayInSeconds) {
        isWaveInProgress = true;
        spawned = false;
        yield return new WaitForSeconds(delayInSeconds);
        StartCoroutine(SpawnWave(enemies, enemyPrefab));
    }

    public IEnumerator SpawnWave(int enemies, GameObject enemyPrefab) {
        isWaveInProgress = true;
        spawned = false;

        for (int i = 0; i < enemies; i++) {
            if (spawner) spawner.Spawn(enemyPrefab);
            else Debug.LogWarning("Spawner is not set.");
            yield return new WaitForSeconds(spawnCooldownInSeconds);
        }
        spawned = true;
    }

    private void Update() {
        if (isPlaying) {
            if (!isWaveInProgress) {
                difficultyLevel += difficultyIncrease;
                GameObject enemyPrefab = enemiesPrefabs[enemiesPrefabs.Length - 1];

                int i = 1;
                while (i <= enemiesPrefabs.Length && enemyPrefab == null) {
                    if (difficultyLevel <= i * difficultyRange)
                        enemyPrefab = enemiesPrefabs[i - 1];
                    i++;
                }

                StartCoroutine(SpawnWaveWithDelay(
                    enemies: (int)difficultyLevel,
                    enemyPrefab: enemyPrefab,
                    delayInSeconds: waveDelayInSeconds));
            }
            if (isWaveInProgress && spawned && !AreEnemiesAlive)
                isWaveInProgress = false;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F4))
            IncreaseScore(1);
    }

    public void Pass() {
        Passes++;
        if (Passes >= maxPasses)
            Lose();
    }

    public void Lose() {
        // TODO: ending
        isPlaying = false;
        Debug.Log("game over");
        Time.timeScale = 0;
        if (gameOverCanvas) gameOverCanvas.enabled = true;
        else Debug.LogWarning("Game-over-canvas is not set.");
    }

    private void OnDestroy() {
        Time.timeScale = 1;
    }

    private void OnGUI() {
        if (moneyText) moneyText.text = money + " " + Currency;
        else Debug.LogWarning("Money text is not set.");
        if (scoreText) scoreText.text = score.ToString();
        else Debug.LogWarning("Score text is not set.");

        string livesString = "";
        for(int i = 0; i<maxPasses - Passes; i++) {
            livesString += "<3";
        }
        livesText.text = livesString;
    }

    public void IncreaseScore(int increase) {
        score += increase;
    }

    public void IncreaseMoney(float increase) {
        money += increase;
    }
}
