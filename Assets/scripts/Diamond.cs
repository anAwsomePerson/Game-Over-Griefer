using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {
    private GameObject goalGO;
    public int speed = 512;
    private Transform targetNode;
    private int something = 0;

	// Use this for initialization
	void Start () {
        goalGO = GameObject.Find("Collector");
	}

    void GetNextNode()
    {
        //Debug.Log("called GetNextNode");
        if (something < 1)
        {
            targetNode = goalGO.transform;
            //ReachedGoal();
        }
        else
        {
            ReachedGoal();
        }

        something ++;
        //Debug.Log(targetPathNode);
    }

    // Update is called once per frame
    void Update () {
        if (targetNode == null)
        {
            GetNextNode();
            //return;
        }

        //Debug.Log(pathNodeIndex);
        if (something < 2)
        {
            Vector3 dir = targetNode.position - this.transform.localPosition;
            float distThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distThisFrame)
            {
                targetNode = null;
            }
            else
            {
                transform.Translate(dir.normalized * distThisFrame, Space.World);
                //this.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    void ReachedGoal()
    {
        GameObject.FindObjectOfType<ScoreManager>().ChangeBalance(1);
        Destroy(gameObject);
    }
}
