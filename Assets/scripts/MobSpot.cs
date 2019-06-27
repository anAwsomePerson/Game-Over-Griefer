using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpot : MonoBehaviour {
    public int tileType;
    private bool occupied = false;
    private GameObject selectedTower;

    public void Unoccupy()
    {
        occupied = false;
    }

    void OnMouseUp()
    {
        if (!occupied)
        {
            //Debug.Log("1");
            BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
            //Debug.Log("2");

            /*if (tileType == 0)
            {
                selectedTower = bm.selectedTower;
            }*/

            if(bm.SelectedTower() == null)
            {
                bm.SelectTowerType(-1);
                return;
            }

            int id = bm.SelectedTower().GetComponent<BaseMob>().id;

            switch (tileType)
            {
                case 0:
                    if ((id != 1) && (id != 5) && (id != 8))
                    {
                        selectedTower = bm.SelectedTower();
                    }
                    else
                    {
                        GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Wrong block!");
                        return;
                    }
                    break;
                case 1:
                    if ((id == 8) || (id == 4) || (id == 6))
                    {
                        selectedTower = bm.SelectedTower();
                    }
                    else
                    {
                        GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Wrong block!");
                        return;
                    }
                    break;
                case 2:
                    if ((id == 1) || (id == 4) || (id == 6))
                    {
                        selectedTower = bm.SelectedTower();
                    }
                    else
                    {
                        GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Wrong block!");
                        return;
                    }
                    break;
                case 3:
                    if ((id == 5) || (id == 4) || (id == 6))
                    {
                        selectedTower = bm.SelectedTower();
                    }
                    else
                    {
                        GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Wrong block!");
                        return;
                    }
                    break;
            }

            if ((selectedTower != null) && ScoreManager.StillPlaying())
            {
                //Debug.Log("3");
                ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
                //Debug.Log("4");
                if (sm.money < selectedTower.GetComponent<BaseMob>().cost)
                {
                    GameObject.FindObjectOfType<ScoreManager>().ChangeCenterText("Not enough money!");
                    return;
                }

                sm.ChangeBalance(selectedTower.GetComponent<BaseMob>().cost * (-1));
                GameObject mobGO = (GameObject)Instantiate(selectedTower, this.transform.position, this.transform.rotation);
                BaseMob mob = mobGO.GetComponent<BaseMob>();
                mob.SetSpot(this);
                bm.StartCooldown(mob.id, mob.buyCooldown);
                //Destroy(transform.parent.gameObject);
                bm.SelectTowerType(-1);
                occupied = true;
            }
        }
    }
}
