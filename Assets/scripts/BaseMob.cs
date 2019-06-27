using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMob : MonoBehaviour {
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
    }

    public void Remove()
    {
        if(id == 7)
        {
            Destroy(GetComponent<Evoker>().trigger);
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
