using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 3;
    private float dmg;

    private void Update() {
        Vector2 move = new Vector2(-speed, 0);
        move *= Time.deltaTime;
        transform.Translate(move);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null) {
            enemy.Hit(dmg);
            Destroy(gameObject);
        }
    }

    public void SetDmg(float newDmg) {
        dmg = newDmg;
    }
}
