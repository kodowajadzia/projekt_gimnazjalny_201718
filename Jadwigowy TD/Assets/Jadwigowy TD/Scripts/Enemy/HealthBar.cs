using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GameObject healthBarPrefab;
    private GameObject healthBarIns;
    public Vector3 offset = new Vector3(0, 1, 0);
    private Image image;
    private float health = 1;

	void Start () {
        healthBarIns = Instantiate(healthBarPrefab);
        healthBarIns.transform.position = transform.position + offset;
        image = healthBarIns.transform.GetChild(0).GetChild(0).GetComponent<Image>();
	}

    public void SetHealth(float current, float max) {
        health = current / max;
    }

    private void Update() {
        healthBarIns.transform.position = transform.position + offset;
        image.fillAmount = health;
    }

    private void OnDestroy() {
        Destroy(healthBarIns);
    }
}
