using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField]
    private int MaxEnemiesNumber = 1; //max enemies number on location
    [SerializeField]
    private float SpawnTime = 2f; //enemies spawn time
    [SerializeField]
    private string[] enemies = new string[] { "Bat", "Bee", "Ghost" };
	
	// Update is called once per frame
	void Update () {

        if (gameObject.transform.childCount < MaxEnemiesNumber) //if enemy died
        {
            var enemy = enemies[Random.Range(0, enemies.Length)];
            StartCoroutine(CreateEnemy(enemy)); //create new enemy
        }

	}

    //create new enemy
    private IEnumerator CreateEnemy(string enemyName)
    {
        var enemy = Instantiate(GetEnemy(enemyName)); //add enemy on scene

        enemy.transform.SetParent(gameObject.transform, false); //set enemy parent
        enemy.transform.position = gameObject.transform.position + GetRandPosition(); //change enemy position
        enemy.SetActive(false); //hide enemy

        yield return new WaitForSeconds(SpawnTime); //wait for spawn

        enemy.SetActive(true); //show enemy
    }

    //get enemy prefab
    private GameObject GetEnemy(string enemyName)
    {
        var enemy = Resources.Load<GameObject>("Prefab/Enemies/" + enemyName);

        return enemy;
    }

    //get random spawn position
    private Vector3 GetRandPosition()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);

        return new Vector3(x, y);
    }
}
