using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int healthPoints = 3;
    public GameObject loot;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            healthPoints--;

            if(healthPoints < 1)
            {
                float xRot = (loot.tag == "Coin") ? -90f : 0f;

                GameObject spawned = Instantiate(loot, transform.position, Quaternion.Euler(xRot, 0f, 0f)) as GameObject;
                Destroy(this.gameObject);
            }
        }
    }
}
