using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private GameObject inventory; //inventory object
    private Dictionary<Vector3, bool> freePositionsInInventory; //inventories position
    private int maxItemCount = 3; //max items in inventory
    private int itemsCount = 3; //current items count in inventory

    private static bool isInventoryOpen = false; //is inventory opened

    public bool IsInventoryOpen
    {
        get
        {
            return isInventoryOpen;
        }
    }


	// Use this for initialization
	void Start () {

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(isInventoryOpen);

        freePositionsInInventory = new Dictionary<Vector3, bool>();
        freePositionsInInventory.Add(new Vector3(-36, 1), true);
        freePositionsInInventory.Add(new Vector3(2, 1), true);
        freePositionsInInventory.Add(new Vector3(38, 1), true);

        StartCoroutine(AddInventory("HealthPotion"));
        StartCoroutine(AddInventory("HealthPotion"));
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
                    if (FindObjectOfType<TaskManagement>().IsJournalOpen)
                        StartCoroutine(FindObjectOfType<TaskManagement>().JournalVisibility());

                    InventoryVisibility();
                }
            }
        }
	}

    public void InventoryVisibility()
    {
        isInventoryOpen = !isInventoryOpen;
        inventory.SetActive(isInventoryOpen);
    }

    public IEnumerator AddInventory(string item)
    {
        if (itemsCount <= maxItemCount)
        {
            var newItem = Instantiate(Resources.Load<GameObject>("Prefab/Inventory/" + item));
            newItem.transform.SetParent(inventory.transform);
            newItem.GetComponent<RectTransform>().localPosition = GetFreePosition();
        }
        else
        {
            GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>().text = "Inventory is full";

            yield return new WaitForSeconds(1f);

            GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>().text = string.Empty;
        }
    }

    public void ItemUse(Vector3 position)
    {
        freePositionsInInventory[position] = true;
    }

    private Vector3 GetFreePosition()
    {
        var freePosition = Vector3.zero;

        foreach (var item in freePositionsInInventory)
        {
            if (item.Value)
            {
                freePosition = item.Key;
                freePositionsInInventory[item.Key] = false;
                break;
            }

        }

        return freePosition;
    }
}
