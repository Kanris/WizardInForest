using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : MonoBehaviour {

    [SerializeField]
    private int Health = 4;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Health == 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
            //Destroy(gameObject);
        }
    }

    public void ManageHealth(int health)
    {
        Health += health;
    }
}
