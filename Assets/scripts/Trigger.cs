using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    private GameObject evoker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEvoker(GameObject objec)
    {
        evoker = objec;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered");
        if(other.gameObject.CompareTag("griefer"))
        {
            if (other.GetComponent<Griefer>().Vulnerable())
            {
                //Debug.Log("triggered");
                evoker.GetComponent<Evoker>().WillShoot();
            }
        }
    }
}
