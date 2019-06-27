using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fangs : MonoBehaviour {
    private float damage;
    private float life = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("griefer"))
        {
            if (other.GetComponent<Griefer>().Vulnerable())
            {
                //Debug.Log("fang triggered");
                other.GetComponent<Griefer>().TakeDamage(damage, 3);
            }
        }
    }

    public void Set(float damageIn)
    {
        damage = damageIn;
    }
}
