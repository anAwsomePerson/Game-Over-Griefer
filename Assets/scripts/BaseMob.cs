using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMob : MonoBehaviour {
    public bool hasRange;
    public int id;
    public int cost;
    public float buyCooldown;
    public int upgradeCost;
    public string abbrev;
    public Material[] materials = new Material[3];
    //public Sprite baseSprite;
    private int level = 0;
    private int spent = 0;
    private MobSpot spot;

    private void OnMouseUp()
    {
        if(ScoreManager.StillPlaying() && upgradeCost > 0)
        {
            BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
            bm.SelectInstance(this);

            switch (id)
            {
                case 0:
                    GetComponent<Skeleton>().RangeGO().SetActive(true);
                    //Debug.Log("set active");
                    break;
                case 2:
                    GetComponent<Witch>().RangeGO().SetActive(true);
                    break;
                case 5:
                    GetComponent<Skeleton>().RangeGO().SetActive(true);
                    break;
            }
        }
    }

    public void Upgrade()
    {
        if(level < 2)
        {
            level++;
            spent += upgradeCost;
            UpdateLevel();
        }
    }

    public void UpdateLevel()
    {
        if(upgradeCost > 0)
        {
            GetComponent<MeshRenderer>().material = Material();
        }

        switch (id)
        {
            case 0:
                GetComponent<Skeleton>().UpdateLevel();
                break;
            case 2:
                GetComponent<Witch>().UpdateLevel();
                break;
            case 3:
                //GetComponent<Skeleton>().UpdateLevel();
                break;
            case 5:
                GetComponent<Skeleton>().UpdateLevel();
                break;
            case 7:
                GetComponent<Evoker>().UpdateLevel();
                break;
            case 8:
                GetComponent<Creeper>().UpdateLevel();
                break;
        }
    }

    public void Remove()
    {
        switch (id)
        {
            case 0:
                GetComponent<Skeleton>().Remove();
                break;
            case 2:
                GetComponent<Witch>().Remove();
                break;
            case 5:
                GetComponent<Skeleton>().Remove();
                break;
            case 7:
                GetComponent<Evoker>().Remove();
                break;
        }

        spot.Unoccupy();
        Destroy(gameObject);
    }

    public void SetSpot(MobSpot inSpot)
    {
        spot = inSpot;
    }

    public Material Material()
    {
        if(upgradeCost > 0)
        {
            return (materials[level]);
        }

        Debug.Log("Someone tried to get my material!");
        return (GetComponent<MeshRenderer>().material);
    }

    public int Level()
    {
        return (level);
    }

    public int Spent()
    {
        return (spent);
    }

    // Use this for initialization
    void Start () {
        if(upgradeCost > 0)
        {
            spent += cost;
            UpdateLevel();
        }
	}
	
	// Update is called once per frame
	void Update () {}
}
