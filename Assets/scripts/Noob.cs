using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noob : MonoBehaviour {
    public GameObject diamond;
    private float lifeRemaining = 100f;
    private float diamondCD = 20f;
    private float diamondCDRemaining = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(ScoreManager.Timer())
        {
            lifeRemaining -= Time.deltaTime;
            diamondCDRemaining -= Time.deltaTime;

            if(diamondCDRemaining <= 0)
            {
                Instantiate(diamond, this.transform.position, this.transform.rotation);
                diamondCDRemaining = diamondCD;
            }

            if(lifeRemaining <= 0)
            {
                BaseMob mob = this.GetComponent<BaseMob>();
                mob.Remove();
            }
        }
	}
}
