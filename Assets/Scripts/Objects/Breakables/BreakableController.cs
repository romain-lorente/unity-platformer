using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
    public GameObject onDestroyParticleEmitter;
    public GameObject onDestroyObjectSpawned;
    public bool destroyIfNotGrounded;
    public bool canBeCrushed = false;
    public float minVelocityToCrush = -10.0f;

    void DestroyBreakable()
    {
        GameObject part = Instantiate(onDestroyParticleEmitter, transform.position, Quaternion.identity) as GameObject;

        if (onDestroyObjectSpawned != null)
        {
            GameObject spawned = Instantiate(onDestroyObjectSpawned, transform.position, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 0.5f) && destroyIfNotGrounded)
            DestroyBreakable();
    }

    void OnCollisionEnter(Collision collider)
    {
        switch(collider.gameObject.tag)
        {
            case "PlayerProjectile":
                DestroyBreakable();
                break;

            case "Player":
                GameObject go = collider.gameObject;
                PlayerController ctrl = go.GetComponent<PlayerController>();

                if (canBeCrushed && (go.transform.position.y > gameObject.transform.position.y) && (ctrl.getPreviousVelocity().y <= minVelocityToCrush))
                {
                    DestroyBreakable();
                }
                break;
        }
    }
}
