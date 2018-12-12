using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public bool enabled { get; set; }

    GameObject player;
    GameObject enemy;
    GameObject gameOverCanvas;

    public int enemyCount = 0;
    private const int MAX_ENEMIES = 10;

    void Awake()
    {
        //spawn our player
        //Vector3 spawnPoint = new Vector3(101f, 32f, -15f);
        player = GameObject.FindGameObjectWithTag("Player");
        //player = (GameObject)Instantiate(playerPrefab, spawnPoint, transform.rotation);
        //SpawnNewEnemy();
        //gameOverCanvas = GameObject.FindGameObjectWithTag("Game Over");
        //gameOverCanvas.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        enabled = player.GetComponent<PlayerController>().enabled;

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetGame();
        //}

        if (!enabled)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync("SampleScene");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
        Debug.Log(enemyCount);
	}

    public void SpawnNewEnemy()
    {
        int spawnAmount = 3;
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 respawnLocation = player.transform.position + (Random.insideUnitSphere * 35);
            respawnLocation.y = 15;
            if (enemyCount < MAX_ENEMIES)
            {
                enemy = (GameObject)Instantiate(enemyPrefab, respawnLocation, transform.rotation);
                enemyCount++;
            } else
            {
                Debug.Log("ENEMY COUNT AT MAX: " + MAX_ENEMIES);
            }
        }
    }

    void ResetGame()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
        Destroy(player);
        Awake();
    }
}
