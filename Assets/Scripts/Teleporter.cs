using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The Teleporter class handles the "hotspots" random teleportation 
 * of anything that steps on it. This means it also includes any GameObject
 * with a collider.
 **/
public class Teleporter : MonoBehaviour {

    public GameObject[] teleporters;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log(other.tag);

            //Vector3 randomTeleport = Random.insideUnitSphere * 15;
            //randomTeleport.y = 1;
            //other.transform.position = randomTeleport;


            int random = Random.Range(0, teleporters.Length);
            other.transform.rotation = teleporters[random].transform.rotation;
            other.transform.position = teleporters[random].transform.position;

        }
    }
}
