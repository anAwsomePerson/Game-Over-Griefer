using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : MonoBehaviour {
    public float damage;
    public float radius;
    public float sellCost;
    public float fireCooldown;
    public Material gunpowder;
    public Material creeper0;
    private bool loaded;
    private float fireCDRemaining;

	// Use this for initialization
	void Start () {
        loaded = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Timer())
        {
            if (loaded)
            {
                Griefer[] griefers = GameObject.FindObjectsOfType<Griefer>();
                Griefer nearestGriefer = null;
                float dist = Mathf.Infinity;

                foreach (Griefer griefer in griefers)
                {
                    if (!griefer.Vulnerable())
                    {
                        continue;
                    }

                    float d = Vector3.Distance(this.transform.position, griefer.transform.position);

                    if (nearestGriefer == null || d < dist)
                    {
                        nearestGriefer = griefer;
                        dist = d;
                    }
                }

                if (nearestGriefer == null)
                {
                    return;
                }

                Vector3 dir = nearestGriefer.transform.position - this.transform.position;
                /*Quaternion lookRot = Quaternion.LookRotation(dir);
                turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);*/
                //fireCooldownLeft -= Time.deltaTime;
                //Debug.Log(dir.magnitude);

                if (dir.magnitude <= radius /*&& ScoreManager.stillPlaying == true*/)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                    foreach (Collider c in colliders)
                    {
                        Griefer griefer = c.GetComponent<Griefer>();

                        if (griefer != null && griefer.GetComponent<Griefer>().Vulnerable())
                        {
                            griefer.GetComponent<Griefer>().TakeDamage(damage);
                        }
                    }

                    loaded = false;
                    fireCDRemaining = fireCooldown;
                    gameObject.GetComponent<MeshRenderer>().material = gunpowder;
                }
            }
            else
            {
                fireCDRemaining -= Time.deltaTime;
                
                if(fireCDRemaining <= 0)
                {
                    loaded = true;
                    gameObject.GetComponent<MeshRenderer>().material = creeper0;
                }
            }
        }
    }
}
