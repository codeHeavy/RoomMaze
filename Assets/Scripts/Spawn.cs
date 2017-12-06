using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    GameObject enemies;
	void Start () {
    }
	
	void Update () {
		
	}

    public void SpawnEnemies(int rows,int columns,float size, GameObject enemyPrefab, int minEnemies, int maxEnemies)
    {
        enemies = new GameObject("Enemies");
        int numEnemies = Random.Range(minEnemies, maxEnemies);
        Debug.Log("Number of Enemies " + numEnemies);
        if (enemyPrefab == null)
        {
            Debug.Log("Add player prefab");
        }
        else
        {
            while (numEnemies != 0)
            {
                GameObject enemyObject = Instantiate(enemyPrefab, new Vector3(Random.Range(0, rows) * size, -0.7f, Random.Range(0, columns) * size), Quaternion.identity);
                enemyObject.transform.name = "enemy " + numEnemies; 
                enemyObject.transform.SetParent(enemies.transform);
                numEnemies--;
            }
        }
    }

    public void SpawnPlayer(GameObject playerPrefab)
    {
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(0 , -0.7f, 0), Quaternion.identity);
        playerObject.transform.name = "playerObject";
    }
}
