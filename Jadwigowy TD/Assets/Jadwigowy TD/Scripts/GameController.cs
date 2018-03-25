using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private uint maxPasses = 5;
    public uint Passes { get; private set; }

    public const string currency = "BTC";
    public float Money { get; set; }
    private int score;// TODO score awarding

    [SerializeField] private Canvas gameOverCanvas;

    private float difficultyLevel;
    [SerializeField] private float difficultyIncrease = 5f, difficultyRange = 100f;
    [SerializeField] private float spawnCooldownInSeconds = 1f,waveDelayInSeconds = 3f;
    [SerializeField] private GameObject[] enemiesPrefabs = new GameObject[0];
    [SerializeField] private EnemySpawner spawner;
    private bool isWaveInProgress, spawned;

    public bool AreEnemiesAlive {
        get { return FindObjectsOfType<Enemy>().Length > 0; }
    }

    public float DifficultyIncrease {
        get { return difficultyIncrease; }
        set { difficultyIncrease = value; }
    }

    public float DifficultyRange {
        get { return difficultyRange; }
        set { difficultyRange = value; }
    }

    public EnemySpawner Spawner {
        get { return spawner; }
        set { spawner = value; }
    }

    public GameObject[] EnemiesPrefabs {
        get { return enemiesPrefabs; }
        set { enemiesPrefabs = value; }
    }

    public uint MaxPasses {
        get { return maxPasses; }
        set { maxPasses = value; }
    }

    public bool IsPlaying { get; set; }

    public float SpawnCooldownInSeconds {
        get { return spawnCooldownInSeconds; }
        set { spawnCooldownInSeconds = value; }
    }

    public float WaveDelayInSeconds {
        get { return waveDelayInSeconds; }
        set { waveDelayInSeconds = value; }
    }

    [SerializeField] private Text moneyText, scoreText;

    private void Start() {
        if (gameOverCanvas)
            gameOverCanvas.enabled = false;
        else
            Debug.LogWarning("Game-over-canvas is not set.");
        IsPlaying = true;
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
            if (Spawner) Spawner.Spawn(enemyPrefab);
            else Debug.LogWarning("Spawner is not set.");
            yield return new WaitForSeconds(SpawnCooldownInSeconds);
        }
        spawned = true;
    }

    private void Update() {
        if (IsPlaying) {
            if (!isWaveInProgress) {
                difficultyLevel += DifficultyIncrease;
                GameObject enemyPrefab = null;

                int i = 1;
                while (i <= EnemiesPrefabs.Length && enemyPrefab == null) {
                    if (difficultyLevel <= i * DifficultyRange)
                        enemyPrefab = EnemiesPrefabs[i - 1];
                    i++;
                }

                StartCoroutine(SpawnWaveWithDelay(
                    enemies: (int)difficultyLevel,
                    enemyPrefab: enemyPrefab,
                    delayInSeconds: WaveDelayInSeconds));
            }
            if (isWaveInProgress && spawned && !AreEnemiesAlive)
                isWaveInProgress = false;
        }
    }

    public void Pass() {
        Passes++;
        if (Passes >= MaxPasses)
            Lose();
    }

    public void Lose() {
        // TODO ending
        IsPlaying = false;
        Debug.Log("game over");
        Time.timeScale = 0;
        if (gameOverCanvas) gameOverCanvas.enabled = true;
        else Debug.LogWarning("Game-over-canvas is not set.");
    }

    private void OnDestroy() {
        Time.timeScale = 1;
    }

    private void OnGUI() {
        if (moneyText) moneyText.text = Money + " " + currency;
        else Debug.LogWarning("Money text is not set.");
        if (scoreText) scoreText.text = score.ToString();
        else Debug.LogWarning("Score text is not set.");
    }
}
