using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMob : MonoBehaviour {
    public int id;
    public int cost;
    public float buyCooldown;
    public int upgradeCost;
    public string abbrev;
    public Sprite baseSprite;
    private MobSpot spot;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
