using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFrames : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float frameCD = 5f;

        if (ScoreManager.StillPlaying())
        {
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
                    griefer.SetFrameCD(frameCD);
                }
            }

            BaseMob mob = this.GetComponent<BaseMob>();
            mob.Remove();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
