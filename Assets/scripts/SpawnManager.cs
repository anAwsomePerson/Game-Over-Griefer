using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    private static int waves = 0;
    public static int totalWaves = 14;
    private static int[] sharedComponents = new int[totalWaves - 1];
    public float waveCD = 30f;
    private static float waveCDRemaining = 30f;
    private static bool spawning = false;
    public GameObject spawner0;
    public GameObject spawner1;
    public GameObject sendButton;
    private static bool didSpawn = false;

    public void SendGriefersNow()
    {
        if (!spawning)
        {
            waveCDRemaining = 0;
        }
    }

    void Start()
    {
        waveCDRemaining = waveCD;

        for (int i = 0; i < totalWaves - 1; i++)
        {
            int grieferID = (int)Random.Range(0, 4);

            while(grieferID == 4)
            {
                grieferID = (int)Random.Range(0, 4);
            }

            sharedComponents[i] = grieferID;
        }

        /*if (sharedComponents[0] == 3)
        {
            Debug.Log("okay");
        }*/
    }

    void Update()
    {
        if (ScoreManager.Timer())
        {
            spawning = spawner1.GetComponent<Spawner>().Spawning();
            sendButton.SetActive(!spawning && (waves < totalWaves));

            if (!spawning && (waves < totalWaves))
            {
                waveCDRemaining -= Time.deltaTime;
                
                if(waves < totalWaves - 1)
                {
                    GameObject.FindObjectOfType<ScoreManager>().changeGriefer(sharedComponents[waves]);
                }

                if(waves == totalWaves - 1)
                {
                    GameObject.FindObjectOfType<ScoreManager>().changeGriefer(4);
                }
            }

            if ((waveCDRemaining < 0) && (waves < totalWaves))
            {
                waves++;
                //Debug.Log("Wave " + waves);
                GameObject.FindObjectOfType<ScoreManager>().waveText.text = "Wave " + waves.ToString() + " of 14";
                spawner0.GetComponent<Spawner>().SetSpawning(true);
                spawner1.GetComponent<Spawner>().SetSpawning(true);
                GameObject.FindObjectOfType<ScoreManager>().changeGriefer(-1);
                waveCDRemaining = waveCD * (1.0f - (float)waves / (float)totalWaves);

                if (waves == totalWaves)
                {
                    GameObject.FindObjectOfType<ScoreManager>().audioSources[0].GetComponent<AudioSource>().Stop();
                    GameObject.FindObjectOfType<ScoreManager>().audioSources[1].GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    public static int Waves()
    {
        return (waves);
    }

    public static int[] SharedComponents()
    {
        return (sharedComponents);
    }

    public static bool Spawning()
    {
        return (spawning);
    }

    public static float WaveCDRemaining()
    {
        return (waveCDRemaining);
    }

    public static bool DidSpawn()
    {
        return (didSpawn);
    }

    public static void SetDidSpawn(bool inDidSpawn)
    {
        didSpawn = inDidSpawn;
    }
}
