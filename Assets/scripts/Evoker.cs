﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evoker : MonoBehaviour {
    public float damage;
    public float fireCooldown;
    private float fireCooldownLeft = 0f;
    public int sellCost;
    public GameObject trigger;
    public GameObject fangsPrefab;
    private bool willShoot = false;
    private GameObject triggerGO;

	// Use this for initialization
	void Start () {
        ResetTrigger();
    }
	
	// Update is called once per frame
	void Update () {
        if (ScoreManager.Timer())
        {
            fireCooldownLeft -= Time.deltaTime;

            if ((fireCooldownLeft <= 0) && willShoot)
            {
                GameObject fangsGO = (GameObject)Instantiate(fangsPrefab, this.transform.position, this.transform.rotation);
                Fangs fangs = fangsGO.GetComponent<Fangs>();
                fangs.Set(damage);
                fireCooldownLeft = fireCooldown;
                willShoot = false;
                Destroy(triggerGO);
                ResetTrigger();
            }
        }
	}

    public void WillShoot()
    {
        willShoot = true;
    }

    public void ResetTrigger()
    {
        triggerGO = (GameObject)Instantiate(trigger, this.transform.position, this.transform.rotation);
        triggerGO.GetComponent<Trigger>().SetEvoker(gameObject);
    }
}