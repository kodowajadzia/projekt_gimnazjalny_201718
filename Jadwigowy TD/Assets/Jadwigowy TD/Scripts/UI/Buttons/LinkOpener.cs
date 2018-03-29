using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour{

    public void Open(string url) {
        Application.OpenURL(url);
    }
}
