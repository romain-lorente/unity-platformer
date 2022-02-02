using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideProjectileController : MonoBehaviour
{
    public int lifespan = 90;

    void CheckDestruction()
    {
        if (lifespan <= 0)
            Destroy(gameObject);
    }
	
	void Update ()
    {
        lifespan--;
        CheckDestruction();
	}

    void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Hazard" && collision.gameObject.tag == "Hazard")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collision>().collider);
        }
        else
        {
            lifespan = 0;
        }
    }
}
