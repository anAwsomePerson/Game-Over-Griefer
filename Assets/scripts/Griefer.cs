using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Griefer : MonoBehaviour
{
    private GameObject pathGO;
    private GameObject goalGO;
    public GameObject damageObject;
    public GameObject diamond;
    public Text weaknessText;
    private Transform targetPathNode;
    private int pathNodeIndex = 0;
    public float baseSpeed;
    private float speed;
    public float baseHealth;
    private float health;
    //int maxIndex = 2;
    //public int moneyValue = 1;
    private int path;
    public float[] effectiveness = new float[4];
    private float slowness = 0f;
    private float frameCDRemaining = 0f;
    private float slownessRemaining = 0f;
    private float weaknessRemaining = 0f;
    private bool vulnerable = false;
    private bool witchTarget = false;
    private AudioSource hurtSource;
    //public GameObject reachedGoalSource;

    // Use this for initialization
    void Start()
    {
        goalGO = GameObject.Find("Goal");
        Spawner[] spawners = GameObject.FindObjectsOfType<Spawner>();
        health = baseHealth + 4 * SpawnManager.Waves() * baseHealth;
        hurtSource = GetComponent<AudioSource>();
        Spawner nearestSpawner = null;
        float distance = Mathf.Infinity;

        foreach (Spawner spawner in spawners)
        {
            float d = Vector3.Distance(this.transform.position, spawner.transform.position);

            if (nearestSpawner == null || d < distance)
            {
                nearestSpawner = spawner;
                distance = d;
            }
        }

        path = nearestSpawner.id;

        if (path == 0)
        {
            pathGO = GameObject.Find("Path0");
        }

        if (path == 1)
        {
            pathGO = GameObject.Find("Path1");
        }
        //Debug.Log(pathGO);

        speed = baseSpeed;
        vulnerable = true;
    }

    void GetNextPathNode()
    {
        //Debug.Log(pathNodeIndex);
        if (pathNodeIndex < pathGO.transform.childCount)
        {
            targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
        }
        else if(pathNodeIndex < pathGO.transform.childCount + 1)
        {
            targetPathNode = goalGO.transform;
            //ReachedGoal();
        }
        else
        {
            ReachedGoal(goalGO);
        }

        pathNodeIndex++;
        //Debug.Log(targetPathNode);
    }

    /*bool checkWitches()
    {
        float divisor = 1f;

        switch (slowness)
        {
            case 0:
                {
                    //speed = baseSpeed;
                    return (false);
                }
            case 1:
                {
                    divisor = effectiveness[1];
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
        }

        Witch[] witches = GameObject.FindObjectsOfType<Witch>();
        Witch nearestWitch = null;
        float dist = Mathf.Infinity;
        bool withinRange = false;

        foreach (Witch witch in witches)
        {
            float d = Vector3.Distance(this.transform.position, witch.transform.position);

            if (nearestWitch == null || d < dist)
            {
                nearestWitch = witch;
                dist = d;
            }

            Vector3 dir = witch.transform.position - this.transform.position;

            if (dir.magnitude < witch.range)
            {
                withinRange = true;
            }
        }

        if ((nearestWitch == null) || !withinRange)
        {
            return (false);
        }

        speed = baseSpeed / divisor;
        //Debug.Log(speed);
        return (true);
    }*/

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Timer())
        {
            speed = baseSpeed;

            if (targetPathNode == null)
            {
                GetNextPathNode();
                return;
            }

            /*if (!checkWitches())
            {
                slowness = 0;
            }*/

            if(slownessRemaining > 0)
            {
                speed = baseSpeed / (effectiveness[1] * slowness);
                slownessRemaining -= Time.deltaTime;

                if(slownessRemaining <= 0)
                {
                    slowness = 0f;
                }
            }

            if (frameCDRemaining > 0)
            {
                speed = 0;
                frameCDRemaining -= Time.deltaTime;
            }

            if(weaknessRemaining > 0)
            {
                weaknessRemaining -= Time.deltaTime;

                if (weaknessRemaining <= 0)
                {
                    weaknessRemaining = 0f;
                    weaknessText.text = "";
                }
            }

            //Debug.Log(pathNodeIndex);
            if (pathNodeIndex <= pathGO.transform.childCount + 1)
            {
                Vector3 dir = targetPathNode.position - this.transform.localPosition;
                float distThisFrame = speed * Time.deltaTime * 7 / 6;

                if (dir.magnitude <= distThisFrame)
                {
                    targetPathNode = null;
                }
                else
                {
                    transform.Translate(dir.normalized * distThisFrame, Space.World);
                    //this.transform.rotation = Quaternion.LookRotation(dir);
                }
            }
        }
    }

    void ReachedGoal(GameObject goalGO)
    {
        /*GameObject soundGO = (GameObject)Instantiate(reachedGoalSource, this.transform.position, this.transform.rotation);

        if (soundGO != null)
        {
            soundGO.GetComponent<AudioSource>().Play();
            //Destroy(soundGO);
        }*/

        GameObject.FindObjectOfType<ScoreManager>().LoseLife(health);
        goalGO.GetComponent<AudioSource>().Play();
        Die(false);
    }

    public void TakeDamage(float damage, int type)
    {
        //Debug.Log(health);
        Instantiate(damageObject, this.transform.position, this.transform.rotation);
        hurtSource.Play();

        if (type < 4)
        {
            damage *= effectiveness[type];

            if(effectiveness[type] > 1){
                //GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("It's super effective!");
                weaknessText.text = "It's super effective!";
                weaknessRemaining = 1f;
            }
        }

        health -= damage;
     
        //Debug.Log(health);
        if (health <= 0)
        {
            Die(true);
        }
    }

    public void TakeDamage(float damage)
    {
        TakeDamage(damage, 4);
    }

    public void Die(bool wasKilled)
    {
        if (wasKilled)
        {
            GameObject.FindObjectOfType<ScoreManager>().ChangeKills();
        }

        Instantiate(diamond, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

    public void SetFrameCD(float time)
    {
        frameCDRemaining = time;
    }

    public float Slowness()
    {
        return (slowness);
    }
    
    public void SetSlowness(float inSlowness)
    {
        slowness = Mathf.Max(inSlowness, slowness);
        slownessRemaining = 1f;
        witchTarget = false;
    }

    public bool Vulnerable()
    {
        return (vulnerable);
    }

    public void SetWitchTarget(bool inTarget)
    {
        witchTarget = inTarget;
    }

    public bool WitchTarget()
    {
        return (witchTarget);
    }
}