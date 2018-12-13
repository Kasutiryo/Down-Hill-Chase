using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem_old : MonoBehaviour {

    //Public Variables
    public int startingWeapon = 0;
    public GameObject[] weaponPrefabs;
    public bool enabled { get; set; }

    GameObject equipedWeapon;

    //Private Variables
    int weaponIndex;

	// Use this for initialization
	void Start () {

        //Sets weapon index to a default value of 0;
        weaponIndex = startingWeapon;

        //Sets starting weapon active
        SetActiveWeapon(weaponIndex);

        enabled = true;

    }
	
	// Update is called once per frame
	void Update () {

        //if disabled, weapons cannot be changed
        if (!enabled) { return; }

        //On mouse right click -> change weapon
        if (Input.GetButtonDown("Fire2"))
            NextWeapon();


    }

    public void SetActiveWeapon(int index)
    {
        // Start be deactivating all weapons
        for (int i = 0; i < weaponPrefabs.Length; i++) { weaponPrefabs[i].SetActive(false); }

        weaponIndex = index;

        // Make weapon visable if the weapon has been unlocked
        if (weaponPrefabs[index].GetComponent<Weapon_old>().unlocked) { weaponPrefabs[index].SetActive(true); }
    }

    public void NextWeapon()
    {
        weaponIndex++;
        if (weaponIndex > weaponPrefabs.Length - 1)
            weaponIndex = 0;

        if (!weaponPrefabs[weaponIndex].GetComponent<Weapon_old>().unlocked) { NextWeapon(); }

        SetActiveWeapon(weaponIndex);

    }

    public int activeWeapon()
    {
        return weaponIndex;
    }
}
