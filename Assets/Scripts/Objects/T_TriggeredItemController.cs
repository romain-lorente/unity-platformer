using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_TriggeredItemController : MonoBehaviour
{
    public int behaviorType;
    public GameObject item1;
    public GameObject item2;

    public void Triggered()
    {
        switch(behaviorType)
        {
            //Item spawn (item1 = spawned item, item2 = particle emitter)
            case 0:
                float xRot = (item1.tag == "Coin") ? -90f : 0f;

                GameObject spawned = Instantiate(item1, transform.position, Quaternion.Euler(xRot, 0f, 0f)) as GameObject;
                GameObject part = Instantiate(item2, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                Destroy(this.gameObject);

                break;
        }
    }
}
