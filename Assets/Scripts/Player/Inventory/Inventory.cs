using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private GameObject inventory;
    private bool isActive = false;

    private List<GameObject> list;

	// Use this for initialization
	void Start () {

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(isActive);

        list = new List<GameObject>(9);

        StartCoroutine(AddInventory("HealthPotion"));
    }
	
	// Update is called once per frame
	void Update () {

        if (!WarpToScene.isWarping && !ScreenFader.isFading) //if player not warping
        {
            if (!PauseMenu.isGamePaused) //game is not paused
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    InventoryVisibility();
                }
            }
        }
	}

    private void InventoryVisibility()
    {
        isActive = !isActive;
        inventory.SetActive(isActive);

        list.ForEach(x => x.SetActive(isActive));
    }

    private IEnumerator AddInventory(string item)
    {
        if (list.Count + 1 < list.Capacity)
        {
            var newItem = Instantiate(Resources.Load<GameObject>("Prefab/Inventory/" + item));
            newItem.transform.SetParent(inventory.transform);
            newItem.GetComponent<RectTransform>().localPosition = new Vector3(-31, 34, 0);

            list.Add(newItem);
        }
        else
        {
            GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>().text = "Inventory is full";

            yield return new WaitForSeconds(1f);

            GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>().text = string.Empty;
        }
    }
}
