using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField]
    private int MaxEnemiesNumber = 1;
    [SerializeField]
    private float SpawnTime = 2f;
	
	// Update is called once per frame
	void Update () {

        if (gameObject.transform.childCount < MaxEnemiesNumber)
        {
            StartCoroutine(CreateEnemy("Bat"));
        }

	}

    private IEnumerator CreateEnemy(string enemyName)
    {
        var enemy = Instantiate(GetEnemy(enemyName));

        enemy.transform.SetParent(gameObject.transform, false);
        enemy.transform.position = gameObject.transform.position + GetRandPosition();
        enemy.SetActive(false);

        yield return new WaitForSeconds(SpawnTime);

        enemy.SetActive(true);
    }

    private GameObject GetEnemy(string enemyName)
    {
        var enemy = Resources.Load<GameObject>("Prefab/" + enemyName);

        return enemy;
    }

    private Vector3 GetRandPosition()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);

        return new Vector3(x, y);
    }
}
