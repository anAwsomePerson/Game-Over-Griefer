using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {
    private GameObject selectedTower;
    public Button cancelButton;
    //public int itemFramesCost = 1;
    //float frameCD = 5;
    //float frameCDRemaining = 0;
    public GameObject[] mobsArray = new GameObject[9];
    public Button[] buttons = new Button[9];
    public Text[] buttonTexts = new Text[9];
    private float[] cooldowns = new float[9];

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        if (ScoreManager.Timer())
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                if (cooldowns[i] > 0)
                {
                    cooldowns[i] -= Time.deltaTime;

                    if(cooldowns[i] <= 0)
                    {
                        buttonTexts[i].text = mobsArray[i].GetComponent<BaseMob>().cost.ToString();
                    }
                }
            }
        }
	}

    public GameObject SelectedTower()
    {
        return (selectedTower);
    }

    public void StartCooldown(int id, float cooldown)
    {
        //Debug.Log(id + " " + cooldown);
        cooldowns[id] = cooldown;
        buttonTexts[id].text = "";
    }

    public void SelectTowerType(int prefab)
    {
        //Debug.Log("clicked");

        if (ScoreManager.StillPlaying())
        {
            if ((prefab >= 0) && (prefab < mobsArray.Length))
            {
                if (GameObject.FindObjectOfType<ScoreManager>().money >= mobsArray[prefab].GetComponent<BaseMob>().cost)
                {
                    if (cooldowns[prefab] <= 0)
                    {
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            //Debug.Log("disabled");
                            buttons[i].gameObject.SetActive(false);
                        }

                        selectedTower = mobsArray[prefab];
                        cancelButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Patience is virtue!");
                    }
                }
                else
                {
                    GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Not enough money!");
                    //Debug.Log(GameObject.FindObjectOfType<ScoreManager>().money + " " + mobsArray[prefab].GetComponent<BaseMob>().cost);
                }
            }
            else
            {
                selectedTower = null;
                cancelButton.gameObject.SetActive(false);

                for(int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
