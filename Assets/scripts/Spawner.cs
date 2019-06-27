using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private float spawnCD = 2f;
    private float spawnCDRemaining;
    private bool spawning = false;
    private bool continueUpdate = true;
    public int id;
    //private static bool didSpawn = false;
    public GameObject griefer0;
    public GameObject griefer1;
    public GameObject griefer2;
    public GameObject griefer3;
    public GameObject youtuber;
    //int spawned = 0;
    private int[] componentCounts = new int[SpawnManager.totalWaves];

    //[System.Serializable]
    /*public class WaveComponent
    {
        //private int number;
        public GameObject grieferPrefab;

        //[System.NonSerialized]
        //public int spawned = 0;

        /*public WaveComponent(int spawnerId)
        {
            id = spawnerId;
        }

        void Update()
        {
            number = ScoreManager.waves + id + 1;
        }

        void Start()
        {
            number = baseNumber + id;
        }

        public WaveComponent(GameObject griefer)
        {
            //number = numberArgument;
            grieferPrefab = griefer;
        }

        /*public int getNumber()
        {
            return (number);
        }
    }*/

    GameObject[] waveComponents = new GameObject[SpawnManager.totalWaves];
    //public static WaveComponent wc = waveComponents[0];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < waveComponents.Length; i++)
        {
            componentCounts[i] = i / 2 + id + 2;

            if (i < SpawnManager.SharedComponents().Length)
            {
                switch (SpawnManager.SharedComponents()[i])
                {
                    case 0:
                        waveComponents[i] = griefer0;
                        break;

                    case 1:
                        waveComponents[i] = griefer1;
                        break;

                    case 2:
                        waveComponents[i] = griefer2;
                        break;

                    case 3:
                        waveComponents[i] = griefer3;
                        break;
                }
            }
        }

        waveComponents[SpawnManager.totalWaves - 1] = youtuber;
        componentCounts[SpawnManager.totalWaves - 1] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Timer() && continueUpdate)
        {
            continueUpdate = false;
            spawnCDRemaining -= Time.deltaTime;
            //Debug.Log("waveCDRemaining: " + waveCDRemaining + " spawning:" + spawning);
            //Debug.Log(waveCDRemaining);

            if ((spawnCDRemaining < 0) && spawning)
            {
                //Debug.Log(id + " " + spawning);
                spawnCDRemaining = spawnCD;
                //Debug.Log("Size: " + waveComponents.Length + " Index: " + (SpawnManager.Waves() - 1));
                GameObject wc = waveComponents[SpawnManager.Waves() - 1];
                //Debug.Log(" didSpawn: " + didSpawn);
                SpawnManager.SetDidSpawn(false);

                if(wc != null)
                {
                    //Debug.Log(wc.spawned + " " + wc.getNumber());
                }

                if (componentCounts[SpawnManager.Waves() - 1] > 0)
                {
                    /*if (id == 1)
                    {
                        Debug.Log(componentCounts[ScoreManager.waves] + " " + spawned);
                    }*/

                    Instantiate(wc, this.transform.position, this.transform.rotation);
                    componentCounts[SpawnManager.Waves() - 1] --;
                    SpawnManager.SetDidSpawn(true);
                }
                    /*else
                    {
                        spawning = false;
                    }

                    if (!didSpawn)
                    {
                        break;
                    }*/

                if (!SpawnManager.DidSpawn())
                {
                    spawning = false;
                    spawnCDRemaining = 0;
                    //spawned = 0;

                    /*if ((ScoreManager.waves < ScoreManager.totalWaves) && (id == 1))
                    {
                        //transform.parent.GetChild(1).gameObject.SetActive(true);
                        ScoreManager.waves++;
                    }*/

                    //Destroy(gameObject);
                }

                //Debug.Log(didSpawn);
            }

            continueUpdate = true;
        }
    }

    void endWave()
    {
        /*spawning = false;

        if (transform.parent.childCount > 1)
        {
            //transform.parent.GetChild(1).gameObject.SetActive(true);
            ScoreManager.waves++;
        }

        //Destroy(gameObject);*/
    }

    public bool Spawning()
    {
        return (spawning);
    }

    public void SetSpawning(bool inSpawning)
    {
        spawning = inSpawning;
    }
}