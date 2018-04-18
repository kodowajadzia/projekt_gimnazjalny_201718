using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;

    //initialization 
    void Start() { }

    // Update 
    void Update()
    {
        if (curHealth < 1)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            curHealth -= 20;
            Destroy(col.other);
        }
    }
}
 