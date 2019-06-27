using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {
    public float life;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
