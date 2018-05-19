using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassEffect : MonoBehaviour {

    public Image passImage;

    public void StartEffect() {
        StartCoroutine(PlayPassAnimation());
    }

    private IEnumerator PlayPassAnimation() {
        // TODO: nice animations or something
        Camera.main.transform.Rotate(0, 0, 5);
        passImage.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.05f);
        Camera.main.transform.Rotate(0, 0, -5);
        passImage.color = new Color(0, 0, 0, 0);
    }
}
