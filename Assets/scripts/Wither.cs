using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wither : MonoBehaviour {

    // Use this for initialization
    void Start() {
        //Debug.Log("3");
        /*ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
        //Debug.Log("4");
        if (sm.money < itemFramesCost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        sm.ChangeBalance(-1 * itemFramesCost);*/
        Griefer[] griefers = GameObject.FindObjectsOfType<Griefer>();

        foreach (Griefer griefer in griefers)
        {
            if (griefer.Vulnerable())
            {
                griefer.TakeDamage(14f);
            }
        }

        BaseMob mob = this.GetComponent<BaseMob>();
        mob.Remove();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
