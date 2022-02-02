using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public float rotationSpeed = 100f;
    public int coinValue = 1;
	
	void Update () {
        float angle = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * angle, Space.World);
    }

    void OnDestroy()
    {
        Manager.Instance.AddCoins(coinValue);
    }
}
