using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNodesController : MonoBehaviour
{
    int currentNode = 0;

    public float movementSpeedPerTick = 2f;
    public bool rotateTowardsNode = true;
    public GameObject[] nodesToFollow;

	void Start()
    {
		if(nodesToFollow[currentNode] == null)
        {
            this.enabled = false;
        }
	}

    void CheckNode(GameObject node)
    {
        if (Mathf.Abs(transform.position.x - node.transform.position.x) < 0.001f && Mathf.Abs(transform.position.z - node.transform.position.z) < 0.001f)
        {
            if (currentNode == nodesToFollow.Length - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode += 1;
            }

            node = nodesToFollow[currentNode];
        }
    }

    void RotateObject(GameObject node)
    {
        if (rotateTowardsNode)
        {
            Vector3 pos = node.transform.position;
            transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));
        }
    }
	
	void Update()
    {
        GameObject node = nodesToFollow[currentNode];

        CheckNode(node);

        Vector3 endPosition = new Vector3(node.transform.position.x, transform.position.y, node.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * movementSpeedPerTick);

        RotateObject(node);
    }
}
