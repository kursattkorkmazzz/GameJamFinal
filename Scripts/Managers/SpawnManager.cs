using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;


using DesignPatterns;
public class SpawnManager : Singleton<SpawnManager>
{
    [System.Serializable]
    public struct PlayerSpawnPoint {
        [SerializeField]public SpawnPointController playerSpawnPoint;
        [SerializeField]public Vector3 cameraSpawnPoint;
    }


    [Header("Enemies")]
    public GameObject GhostEnemy;

    [Header("Farm Map")]
    public PlayerSpawnPoint farmPlayerSpawnPoint;
    public SpawnPointController[] farmEnemySpawnPoints;

    [Header("Lab Map")]
    public PlayerSpawnPoint labPlayerSpawnPoint;
    public SpawnPointController[] labEnemySpawnPoints;

    [Header("Castle Map")]
    public PlayerSpawnPoint castlePlayerSpawnPoint;
    public SpawnPointController[] castleEnemySpawnPoints;
    private void Awake()
    {
        InitializeSingletonAwake();

    
    }



    public List<GameObject> SpawnEnemy<T>(SpawnPointController[] spawnAreas, int amount = 1) where T : Enemy
    {

        List<GameObject> enemies = new();

        for (int i = 0; i < amount; i++)
        {
            // Selecting random spawn point.
            int selectedSpawnArea = UnityEngine.Random.Range(0, spawnAreas.Length);


            // Calculation random position.
            float selectedPositionX = UnityEngine.Random.Range(spawnAreas[selectedSpawnArea].transform.position.x + spawnAreas[selectedSpawnArea].SpawnPointArea.x/2,
            spawnAreas[selectedSpawnArea].transform.position.x - spawnAreas[selectedSpawnArea].SpawnPointArea.x/2);
            float selectedPositionY = UnityEngine.Random.Range(spawnAreas[selectedSpawnArea].transform.position.y + spawnAreas[selectedSpawnArea].SpawnPointArea.y/2,
            spawnAreas[selectedSpawnArea].transform.position.y - spawnAreas[selectedSpawnArea].SpawnPointArea.y/2);


            Vector3 position = new Vector3(selectedPositionX, selectedPositionY);


            // Find the enemy type from array.



           GameObject newEnemy = Instantiate(GhostEnemy, position, GhostEnemy.transform.rotation);
            enemies.Add(newEnemy);
        }

        return enemies;
    }



    public void TeleportPlayerToExactly(GameObject player,PlayerSpawnPoint playerSpawnPoint)
    {
        player.transform.position = playerSpawnPoint.playerSpawnPoint.transform.position;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = playerSpawnPoint.cameraSpawnPoint;

    }





}
