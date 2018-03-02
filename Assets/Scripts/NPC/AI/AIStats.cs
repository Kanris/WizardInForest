using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIStats : MonoBehaviour {

    [SerializeField]
    private int MaxHealth = 4;
    private int CurrentHealth = 4;

    [SerializeField]
    private TextMesh[] healthBar;

    // Use this for initialization
    void Start () {
        DisplayHealth();
    }
	
	// Update is called once per frame
	void Update () {

        if (CurrentHealth == 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void ManageHealth(int health)
    {
        CurrentHealth += health;
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        if (healthBar != null)
        {
            foreach (var item in healthBar)
            {
                item.text = CurrentHealth + "/" + MaxHealth;
            }
        }
    }
}
