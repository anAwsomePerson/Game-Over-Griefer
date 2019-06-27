using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpowder : MonoBehaviour {
    public GameObject creeper;
    public float fireCooldown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        fireCooldown -= Time.deltaTime;

        if(fireCooldown <= 0)
        {
            Instantiate(creeper, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
	}
}
