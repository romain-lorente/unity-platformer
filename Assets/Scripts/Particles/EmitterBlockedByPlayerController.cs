using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterBlockedByPlayerController : MonoBehaviour
{
    private ParticleSystem ps;
    private Vector3 pos;

	// Use this for initialization
	void Start ()
    {
        ps = GetComponentsInChildren<ParticleSystem>()[0];
        Renderer rd = GetComponent<Renderer>();

        pos = rd.bounds.center;
    }
	
	// Update is called once per frame
	void LateUpdate()
    {
        Collider[] col = Physics.OverlapSphere(pos, 0.3f);
        bool doBlock = false;

        foreach(Collider c in col)
        {
            doBlock = c.tag == "Player" ? true : doBlock;
        }

        if (doBlock)
        {
            ps.Stop();
        }
        else
        {
            ps.Play();
        }
	}
}
