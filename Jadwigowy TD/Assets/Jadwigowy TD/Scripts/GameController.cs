﻿using System.Collections;
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

    public Canvas winCanvas, loseCavnas, resetCanvas;
    public float resetCanvasDuration = 1f;

    private float difficultyLevel;
    public float difficultyIncrease = 5f, difficultyRange = 100f;
    public float spawnCooldownInSeconds = 1f, waveDelayInSeconds = 3f;
    public GameObject[] enemiesPrefabs = new GameObject[0];
    public GameObject boss;
    public int wavesToWin = 35, wavesToResetTowers = 10;
    public Spawner spawner;
    private bool isWaveInProgress, spawned;
    public bool isPlaying;
    public Text moneyText, scoreText, livesText, wavesText;
    private int wave = 1;

    public AudioClip passSound, loseSound, winSound, startWaveSound;
    private AudioSource audioSource;

    public bool AreEnemiesAlive {
        get { return FindObjectsOfType<Enemy>().Length > 0; }
    }

    private void Awake() {
        money = moneyAtStart;
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    private void Start() {
        if (loseCavnas)
            loseCavnas.enabled = false;
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
        audioSource.PlayOneShot(startWaveSound);
        wave++;
        spawned = false;

        for (int i = 0; i < enemies; i++) {
            if (spawner) spawner.Spawn(enemyPrefab);
            else Debug.LogWarning("Spawner is not set.");
            yield return new WaitForSeconds(spawnCooldownInSeconds);
        }
        spawned = true;
    }

    public IEnumerator SpawnBossWithDelay(float delayInSeconds) {
        spawned = false;
        wave++;
        yield return new WaitForSeconds(delayInSeconds);
        SpawnBoss();
    }

    public void SpawnBoss() {
        spawned = false;
        spawner.Spawn(boss);
        spawned = true;
    }

    private void Update() {
        if (isPlaying) {
            if (!isWaveInProgress) {
                if (wave % wavesToResetTowers == 0)
                    DestroyAllTowers();

                if (wave == wavesToWin) {
                    Win();
                }
                //Debug.Log(wave + " " + wavesToWin);
                if (wave == wavesToWin-1) {
                    isWaveInProgress = true;
                    StartCoroutine(SpawnBossWithDelay(waveDelayInSeconds));
                } else {
                    isWaveInProgress = true;
                    difficultyLevel += difficultyIncrease;

                    GameObject enemyPrefab = enemiesPrefabs[enemiesPrefabs.Length - 1];

                    bool choosed = false;
                    int i = 1;
                    while (i <= enemiesPrefabs.Length && !choosed) {
                        if (difficultyLevel <= i * difficultyRange) {
                            enemyPrefab = enemiesPrefabs[i - 1];
                            choosed = true;
                        }
                        i++;
                    }

                    StartCoroutine(SpawnWaveWithDelay(
                        enemies: (int)difficultyLevel,
                        enemyPrefab: enemyPrefab,
                        delayInSeconds: waveDelayInSeconds));
                }
            }
            if (isWaveInProgress && spawned && !AreEnemiesAlive)
                isWaveInProgress = false;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F4) && Input.GetKeyDown(KeyCode.Alpha2))
            IncreaseScore(1);
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F4))
            Application.Quit();
        if (Input.GetKey(KeyCode.RightControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F4) && Input.GetKeyDown(KeyCode.Alpha2))
            money += 1;
        if (Input.GetKeyDown(KeyCode.M))
            wave = 32;
    }

    public void DestroyAllTowers() {
        StartCoroutine(ShowResetCanvas());
        GameObject[] towers = GameObject.FindGameObjectsWithTag(Tower.Tag);
        foreach (GameObject tower in towers) {
            Destroy(tower);
        }
    }

    public IEnumerator ShowResetCanvas() {
        resetCanvas.enabled = true;
        yield return new WaitForSeconds(resetCanvasDuration);
        resetCanvas.enabled = false;
    }

    public void Win() {
        audioSource.PlayOneShot(winSound);
        winCanvas.enabled = true;
        EndGame();
    }

    public void Pass() {
        audioSource.PlayOneShot(passSound);
        Passes++;
        if (Passes >= maxPasses)
            Lose();
    }

    public void Lose() {
        audioSource.PlayOneShot(loseSound);
        if (loseCavnas) loseCavnas.enabled = true;
        else Debug.LogWarning("Game-over-canvas is not set.");
        EndGame();
    }

    public void EndGame() {
        isPlaying = false;
        Time.timeScale = 0;
    }

    private void OnDestroy() {
        Time.timeScale = 1;
    }

    private void OnGUI() {
        if (moneyText) moneyText.text = money + " " + Currency;
        else Debug.LogWarning("Money text is not set.");
        if (scoreText) scoreText.text = score.ToString();
        else Debug.LogWarning("Score text is not set.");
        wavesText.text = "LEKCJA: " + wave;

        string livesString = "";
        for (int i = 0; i < maxPasses - Passes; i++) {
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
