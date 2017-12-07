using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    GameObject enemies;
	void Start () {
    }
	
	void Update () {
		
	}

    /// <summary>
    /// Spawn random number of enemies at random cells in the maze
    /// TODO: exclude dead ends
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="columns"></param>
    /// <param name="size"></param>
    /// <param name="enemyPrefab"></param>
    /// <param name="minEnemies"></param>
    /// <param name="maxEnemies"></param>
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

    /// <summary>
    /// Spawn player at the start position ( cell (0,0) )
    /// </summary>
    /// <param name="playerPrefab"></param>
    public void SpawnPlayer(GameObject playerPrefab)
    {
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(0 , -0.7f, 0), Quaternion.identity);
        playerObject.transform.name = "playerObject";
    }
}
