using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public float lives = 70f;
    public int money = 5;
    public Image nextGriefer;
    public Text moneyText;
    public Text livesText;
    public Text waveText;
    public Text timerText;
    public Text centerText;
    public Sprite[] grieferSprites = new Sprite[5];
    private static bool timer = true;
    private static bool stillPlaying = true;
    private float centerCDRemaining = 0f;
    private int kills = 0;
    public GameObject[] audioSources = new GameObject[4];

    void Start()
    {
        livesText.text = "Stability: " + lives.ToString();
        moneyText.text = "Diamonds: " + money.ToString();
    }

    public void LoseLife(float life)
    {
        lives -= life;

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        //Debug.Log(lives);
        livesText.text = "Stability: " + lives.ToString();
    }

    public void ChangeBalance(int change)
    {
        int potentialChange = money + change;

        if(potentialChange >= 0)
        {
            money = potentialChange;
            moneyText.text = "Diamonds: " + money.ToString();
        }
        else
        {
            ChangeCenterText("Not enough money!");
        }
    }

    public void Pause()
    {
        if (stillPlaying)
        {
            timer = !timer;
        }
    }

    public void changeGriefer(int input)
    {
        if(input >= 0 && input < grieferSprites.Length)
        {
            nextGriefer.sprite = grieferSprites[input];
            return;
        }

        nextGriefer.sprite = null;
    }

    public void GameOver()
    {
        stillPlaying = false;
        timer = false;
        ChangeCenterText("You lose! \nScore: " + (2 * lives + kills));
        audioSources[0].GetComponent<AudioSource>().Stop();
        audioSources[1].GetComponent<AudioSource>().Stop();
        //audioSources[2].GetComponent<AudioSource>().Play();
    }

    public static bool Timer()
    {
        return (timer);
    }

    public static bool StillPlaying()
    {
        return (stillPlaying);
    }

    public void ChangeCenterText(string inText)
    {
        centerText.text = inText;
        centerCDRemaining = 1f;
    }

    void Update()
    {
        Griefer[] griefers = GameObject.FindObjectsOfType<Griefer>();
        //Debug.Log(timer);

        if (timer)
        {
            if (SpawnManager.Spawning() || (SpawnManager.Waves() >= SpawnManager.totalWaves))
            {
                timerText.text = "...";
            }else{
                timerText.text = Mathf.Ceil(SpawnManager.WaveCDRemaining()).ToString();
            }

            if(centerCDRemaining > 0)
            {
                centerCDRemaining -= Time.deltaTime;
            }
            else
            {
                centerText.text = "";
            }
        }

        if((griefers.Length == 0) && (SpawnManager.Waves() >= SpawnManager.totalWaves) && (lives > 0) && stillPlaying)
        {
            ChangeCenterText("You win! \nScore: " + (2 * lives + kills));
            stillPlaying = false;
            audioSources[0].GetComponent<AudioSource>().Stop();
            audioSources[1].GetComponent<AudioSource>().Stop();
            audioSources[3].GetComponent<AudioSource>().Play();
        }
    }

    public void ChangeKills()
    {
        kills ++;
    }

    public void DebugClick()
    {
        //Debug.Log("clicked");
    }
}
