using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public AudioClip hurtClip;                                  // The audio clip to play when the player is damaged.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    PlayerController playerController;
    GameObject[] weapons;
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    void Awake()
    {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        playerController = GetComponent<PlayerController>();
        playerAudio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

        if(Input.GetKeyDown(KeyCode.K)) { TakeDamage(10); }
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        if (isDead) return;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>().text = "" + currentHealth;
        playerAudio.clip = hurtClip;
        playerAudio.Play();
        if(currentHealth<= 0 && !isDead)
        {
            Death();
        }
    }

    public void RestoreHealth(int amount)
    {
        if (currentHealth <= 0 || currentHealth >= 100) return;
        currentHealth += amount;
        healthSlider.value = currentHealth;
        GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>().text = "" + currentHealth;
    }

    void Death()
    {
        isDead = true;
        playerAudio.clip = deathClip;
        playerAudio.loop = true;
        playerAudio.Play();
        playerController.enabled = false;
        //foreach (GameObject weapon in weapons) {
        //    weapon.GetComponent<Weapon_old>().enabled = false;
        //}
        GameObject.FindGameObjectWithTag("WeaponSystem").GetComponent<WeaponSystem_old>().enabled = false;
        playerController.deathAnimation();
        GameObject.FindGameObjectWithTag("UI").GetComponent<Animator>().SetBool("IsDead", true);
        
    }

}
