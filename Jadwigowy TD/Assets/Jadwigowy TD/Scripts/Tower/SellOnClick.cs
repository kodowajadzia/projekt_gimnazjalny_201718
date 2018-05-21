using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellOnClick : MonoBehaviour {

    public float price;

    public AudioClip sellSound;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    private void OnMouseDown() {
        Sell();
    }

    public void Sell() {
        audioSource.PlayOneShot(sellSound);
        FindObjectOfType<GameController>().IncreaseMoney(price);
        Destroy(gameObject);
    }
}
