﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    //Transform turretTransform;
    public GameObject arrowPrefab;
    public float fireCooldown;
    private float fireCooldownLeft = 0f;
    public float damage;
    public float radius;
    public int type;
    public int range;
    //public int sellCost;

    // Use this for initialization
    void Start()
    {
		/*shootSource = GetComponent<AudioSource> ();
        turretTransform = transform.Find("Turret");
        Instantiate(shootPrefab, this.transform.position, this.transform.rotation);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Timer())
        {
            Griefer[] griefers = GameObject.FindObjectsOfType<Griefer>();
            Griefer nearestGriefer = null;
            float dist = Mathf.Infinity;

            foreach(Griefer griefer in griefers)
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
            fireCooldownLeft -= Time.deltaTime;

            //Debug.Log(dir.magnitude);

            if (fireCooldownLeft <= 0 && dir.magnitude <= range /*&& ScoreManager.stillPlaying == true*/)
            {
                fireCooldownLeft = fireCooldown;
                ShootAt(nearestGriefer);
            }
        }
    }

    void ShootAt(Griefer griefer)
    {
        GameObject bulletGO = (GameObject)Instantiate(arrowPrefab, this.transform.position, this.transform.rotation);
        Arrow arrow = bulletGO.GetComponent<Arrow>();
        arrow.Set(griefer.transform, damage, type);
		//shootSource.Play();
    }
}