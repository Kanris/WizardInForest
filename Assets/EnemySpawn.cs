using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField]
    private int MaxEnemiesNumber = 1;

    private int currentEnemiesNumber = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.childCount < MaxEnemiesNumber)
        {
            CreateEnemy("Bat");
        }

	}

    private void CreateEnemy(string enemyName)
    {
        var enemy = Instantiate(GetEnemy(enemyName));

        enemy.transform.SetParent(gameObject.transform, false);
        enemy.transform.position = gameObject.transform.position;

        currentEnemiesNumber++;
    }

    private GameObject GetEnemy(string enemyName)
    {
        var enemy = Resources.Load<GameObject>("Prefab/" + enemyName);

        return enemy;
    }
}
