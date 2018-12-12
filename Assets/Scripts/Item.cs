using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The Item class controls what is done with the any
 * pick up items, such the med kits.
 **/
public class Item : MonoBehaviour {

    public int healAmount;
    public AudioClip soundFX;
    public GameObject inventoryUI;

    AudioSource player;


    void Start()
    {
        player = GetComponent<AudioSource>();
    }
    /**
     * Rotates object in the y-axis, as if spinning to create animation
     **/
    void Update () {
        transform.Rotate(0, 1, 0);
	}

    /**
     * function to determine pick-up action
     **/ 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { Debug.Log("NOT PLAYER"); return; }

        Debug.Log(this.tag);
        if (this.tag == "MedKit")
        {
            player.clip = soundFX;
            player.Play();
            other.GetComponent<PlayerHealth>().RestoreHealth(healAmount);
            Destroy(gameObject, 1f);
        } else if (this.tag == "Weapon")
        {
            player.clip = soundFX;
            player.Play();
            string weaponToEnable = this.name;
            WeaponSystem_old weapons = GameObject.FindGameObjectWithTag("WeaponSystem").GetComponent<WeaponSystem_old>();
            GameObject[] objects = weapons.weaponPrefabs;
            for (int i = 0; i < objects.Length; i++) 
            {
                Debug.Log(objects[i].name + " =? " + weaponToEnable);
                if (objects[i].name.Equals(weaponToEnable))
                {
                    objects[i].GetComponent<Weapon_old>().unlocked = true;
                    weapons.SetActiveWeapon(i);
                    Destroy(gameObject, 1f);
                    return;
                }
            }
        } else if (this.tag == "Objective")
        {
            player.clip = soundFX;
            player.Play();
            other.GetComponent<PlayerController>().setObjectiveStatus(true);
            inventoryUI.SetActive(true);
            Destroy(gameObject, 1f);
        }


    }
}