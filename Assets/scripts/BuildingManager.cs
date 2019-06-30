using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {
    public Button cancelButton;
    public Button upgradeButton;
    public Button sellButton;
    public Text upgradeText;
    public Text sellText;
    public Image selectedImage;
    //public int itemFramesCost = 1;
    //float frameCD = 5;
    //float frameCDRemaining = 0;
    public GameObject[] mobsArray = new GameObject[9];
    public Button[] buttons = new Button[9];
    public Text[] buttonTexts = new Text[9];
    public Sprite[] sprites = new Sprite[9];
    private float[] cooldowns = new float[9];
    private GameObject selectedPrefab;
    private BaseMob selectedInstance;

    // Use this for initialization
    void Start () {}

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Timer())
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                if (cooldowns[i] > 0)
                {
                    cooldowns[i] -= Time.deltaTime;

                    if (cooldowns[i] <= 0)
                    {
                        buttonTexts[i].text = mobsArray[i].GetComponent<BaseMob>().cost.ToString();
                    }
                }
            }
        }
    }

    public void SelectTowerType(int prefab)
    {
        //Debug.Log("clicked");

        if (ScoreManager.StillPlaying())
        {
            if ((prefab >= 0) && (prefab < mobsArray.Length))
            {
                if (GetComponent<ScoreManager>().money >= mobsArray[prefab].GetComponent<BaseMob>().cost)
                {
                    if (cooldowns[prefab] <= 0)
                    {
                        selectedImage.sprite = sprites[prefab];
                        EnableSelect(false);
                        selectedPrefab = mobsArray[prefab];
                    }
                    else
                    {
                        GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Patience is virtue!");
                    }
                }
                else
                {
                    GetComponent<ScoreManager>().ChangeCenterText("Not enough money!");
                    //Debug.Log(GameObject.FindObjectOfType<ScoreManager>().money + " " + mobsArray[prefab].GetComponent<BaseMob>().cost);
                }
            }
            else
            {
                selectedPrefab = null;
                selectedInstance = null;
                upgradeButton.gameObject.SetActive(false);
                sellButton.gameObject.SetActive(false);
                EnableSelect(true);
            }
        }
    }

    public void SelectInstance(BaseMob instance)
    {
        selectedInstance = instance;
        selectedImage.sprite = sprites[instance.id];
        EnableSelect(false);
        sellText.text = "Sell (" + (int)(selectedInstance.Spent() / 2.0) + ")";
        sellButton.gameObject.SetActive(true);

        if(selectedInstance.Level() < 2)
        {
            upgradeText.text = "Upgrade (" + selectedInstance.upgradeCost + ")";
            upgradeButton.gameObject.SetActive(true);
        }
    }

    public void Upgrade()
    {
        if (ScoreManager.StillPlaying() && selectedInstance.Level() < 2)
        {
            if (GetComponent<ScoreManager>().money >= selectedInstance.upgradeCost)
            {
                GetComponent<ScoreManager>().ChangeBalance((-1) * selectedInstance.upgradeCost);
                selectedInstance.Upgrade();
                upgradeButton.gameObject.SetActive(false);
                SelectInstance(selectedInstance);
            }
            else
            {
                GetComponent<ScoreManager>().ChangeCenterText("Not enough money!");
                //Debug.Log(GameObject.FindObjectOfType<ScoreManager>().money + " " + mobsArray[prefab].GetComponent<BaseMob>().cost);
            }
        }
    }

    public void Sell()
    {
        GetComponent<ScoreManager>().ChangeBalance((int)(selectedInstance.Spent() / 2.0));
        SelectTowerType(-1);
        selectedInstance.Remove();
    }

    public GameObject SelectedTower()
    {
        return (selectedPrefab);
    }

    public void StartCooldown(int id, float cooldown)
    {
        //Debug.Log(id + " " + cooldown);
        cooldowns[id] = cooldown;
        buttonTexts[id].text = "";
    }

    public void EnableSelect(bool enable)
    {
        cancelButton.gameObject.SetActive(!enable);
        selectedImage.gameObject.SetActive(!enable);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(enable);
        }
    }
}
