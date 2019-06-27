using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject audioPrefab;
    private int speed = 256;
    private Transform target;
    private float slowness;

    // Use this for initialization
    void Start()
    {
        GameObject audioGO = (GameObject)Instantiate(audioPrefab, this.transform.position, this.transform.rotation);
        audioGO.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Timer())
        {
            //Debug.Log(target);

            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - this.transform.localPosition;
            //Debug.Log(speed);
            float distThisFrame = speed * Time.deltaTime;

            Griefer[] griefers = GameObject.FindObjectsOfType<Griefer>();
            Griefer nearestGriefer = null;

            foreach (Griefer griefer in griefers)
            {
                //float d = Vector3.Distance(this.transform.position, griefer.transform.position);

                if (nearestGriefer == null)
                {
                    nearestGriefer = griefer;
                }
            }

            if (nearestGriefer == null)
            {
                return;
            }

            if (dir.magnitude <= distThisFrame)
            {
                DoBulletHit(target.gameObject);
            }
            else
            {
                transform.Translate(dir.normalized * distThisFrame, Space.World);
                //this.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered");

        if (other.tag == "griefer")
        {
            if(other.GetComponent<Griefer>.slowness())
        }
    }*/

    void DoBulletHit(GameObject target)
    {
        //if (radius == 0)
        //{
        target.GetComponent<Griefer>().SetSlowness(slowness);
        /*}
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider c in colliders)
            {
                Griefer griefer = c.GetComponent<Griefer>();

                if (griefer != null)
                {
                    griefer.GetComponent<Griefer>().TakeDamage(damage, type);
                }
            }
        }*/
        //enemy.TakeDamage(damage);
        Destroy(gameObject);
    }

    public void Set(Transform inTarget, float inSlowness)
    {
        target = inTarget;
        slowness = inSlowness;
    }
}
