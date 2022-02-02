using Assets.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_CoinSpawner : ITriggerable
{
    public GameObject coin;
    public GameObject particle;

    void Start()
    {
		
	}

    public override void Triggered()
    {
        GameObject spawnCoin = Instantiate(coin, transform.position, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
        GameObject spawnParticle = Instantiate(particle, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        Destroy(this.gameObject);
    }

    public override void Untriggered()
    {

    }
}
