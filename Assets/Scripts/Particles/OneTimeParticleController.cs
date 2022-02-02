using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeParticleController : MonoBehaviour
{
    private ParticleSystem ps;

	// Use this for initialization
	void Start ()
    {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ps != null && !ps.IsAlive())
            Destroy(gameObject);
	}
}
