using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public void Spawn(GameObject sample) {
        GameObject ins = Instantiate(sample);
        ins.transform.position = transform.position;
    }
}
