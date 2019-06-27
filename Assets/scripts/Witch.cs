using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour {
    public float slowness;
    public GameObject potionPrefab;
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
            /*Griefer nearestGriefer = null;
            float dist = Mathf.Infinity;*/

            foreach (Griefer griefer in griefers)
            {
                if (!griefer.Vulnerable() || griefer.Slowness() >= slowness || griefer.WitchTarget())
                {
                    continue;
                }

                /*float d = Vector3.Distance(this.transform.position, griefer.transform.position);

                if (nearestGriefer == null || d < dist)
                {
                    nearestGriefer = griefer;
                    dist = d;
                }*/

                Vector3 dir = griefer.transform.position - this.transform.position;

                if(dir.magnitude <= range)
                {
                    ShootAt(griefer);
                }
            }

            /*if (nearestGriefer == null)
            {
                return;
            }

            Vector3 dir = nearestGriefer.transform.position - this.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
            //fireCooldownLeft -= Time.deltaTime;

            //Debug.Log(dir.magnitude);

            if (fireCooldownLeft <= 0 && dir.magnitude <= range /*&& ScoreManager.stillPlaying == true)
            {
                fireCooldownLeft = fireCooldown;
                ShootAt(nearestGriefer);
            }*/
        }
    }

    void ShootAt(Griefer griefer)
    {
        griefer.SetWitchTarget(true);
        GameObject bulletGO = (GameObject)Instantiate(potionPrefab, this.transform.position, this.transform.rotation);
        Potion potion = bulletGO.GetComponent<Potion>();
        potion.Set(griefer.transform, slowness);
        //shootSource.Play ();
    }
}
