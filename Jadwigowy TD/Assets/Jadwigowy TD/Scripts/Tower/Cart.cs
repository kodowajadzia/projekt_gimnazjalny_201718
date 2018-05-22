using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour {

    private static bool isAnySpawned;

    public GameObject caretaker;
    public float range = 1f;
    public float cooldown = 3f;
    public int maxCaretakers = 3;

    private float timer;
    private int caretakersCount;
    private GameObject[] caretakers;

    public bool onlyOne = true;
    public float cannotPutReveune;

    private void Start() {
        if (onlyOne && isAnySpawned) {
            Destroy(gameObject);
            FindObjectOfType<GameController>().IncreaseMoney(cannotPutReveune);
        } else {
            isAnySpawned = true;
        }
        caretakers = new GameObject[maxCaretakers];
        SpawnCaretaker(GetRandomPositionInRange());
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > cooldown) {
            timer = 0;
            if (caretakersCount < maxCaretakers)
                SpawnCaretaker(GetRandomPositionInRange());
            else
                RelocateRandomCaretaker(GetRandomPositionInRange());
        }
    }

    public void SpawnCaretaker(Vector2 pos) {
        GameObject ins = Instantiate(caretaker);
        ins.transform.position = pos;

        caretakers[caretakersCount] = ins;
        caretakersCount++;
    }

    public void RelocateRandomCaretaker(Vector2 pos) {
        caretakers[Random.Range(0, caretakersCount)].transform.position = pos;
    }

    public Vector2 GetRandomPositionInRange() {
        float x = Random.Range(-range, range);
        float y = Random.Range(-range, range);
        return new Vector2(x, y) + (Vector2)transform.position;
    }

    private void OnDestroy() {
        isAnySpawned = FindObjectsOfType<Cart>().Length > 0;
        for (int i = 0; i < caretakersCount; i++) {
            Destroy(caretakers[i]);
        }
    }
}
