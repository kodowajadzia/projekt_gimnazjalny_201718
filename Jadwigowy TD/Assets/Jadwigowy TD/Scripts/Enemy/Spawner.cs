using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Spawn(GameObject sample) {
        GameObject ins = Instantiate(sample);
        ins.transform.position = transform.position;
        return ins;
    }
}
