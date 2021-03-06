﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [SerializeField]
    private GameObject inventory; //inventory object

    private Dictionary<Vector3, bool> freePositionsInInventory; //inventories position
    private int maxItemCount = 3; //max items in inventory
    private int itemsCount = 1; //current items count in inventory

    private int coinsAmount = 400;
    [SerializeField]
    private Text textCoinsAmount;

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

        freePositionsInInventory = new Dictionary<Vector3, bool>(); //create inventory position
        freePositionsInInventory.Add(new Vector3(-36, 1), true);
        freePositionsInInventory.Add(new Vector3(2, 1), true);
        freePositionsInInventory.Add(new Vector3(38, 1), true);

        AddCoins(100);

        //add health potions in inventory
        StartCoroutine(AddInventory("HealthPotion"));
    }
	
	// Update is called once per frame
	void Update () {

        if (!WarpToScene.isWarping && !ScreenFader.isFading) //if player not warping
        {
            if (!PauseMenu.isGamePaused) //game is not paused
            {
                if (Input.GetKeyDown(KeyCode.I)) //if I button clicked
                {
                    if (FindObjectOfType<TaskManagement>().IsJournalOpen) //if journal is open
                        StartCoroutine(FindObjectOfType<TaskManagement>().JournalVisibility()); //close journal

                    InventoryVisibility(); //show inventory
                }
            }
        }
	}

    //show or hide inventory
    public void InventoryVisibility()
    {
        isInventoryOpen = !isInventoryOpen;
        inventory.SetActive(isInventoryOpen);
    }

    //add new item to the inventory
    public IEnumerator AddInventory(string item, GameObject itemGO = null)
    {
        //if inventory is not full
		if (!IsInventoryFull())
        {

            if (itemGO)
            {
                itemGO.GetComponentInChildren<SpriteRenderer>().enabled = false;
                itemGO.GetComponent<EnvironmentInventoryItem>().enabled = false;
                itemGO.GetComponent<BoxCollider2D>().enabled = false;
            }

            //search prefab in inventory
            var newItem = Instantiate(Resources.Load<GameObject>("Prefab/Inventory/" + item));
            newItem.transform.SetParent(inventory.transform);
            newItem.GetComponent<RectTransform>().localPosition = GetFreePosition(); //add item to the free position

            ++itemsCount;

            string itemAddString = "Add " + item + " to inventory";
            yield return FindObjectOfType<HUDAnnouncer>().DisplayAnnounce(itemAddString, 1f);

            Destroy(itemGO);

        }
        else //inventory is full
        {
            yield return FindObjectOfType<HUDAnnouncer>().DisplayAnnounce("Inventory is full", 2f);
        }

    }

    //item used
    public void ItemUse(Vector3 position)
    {
        freePositionsInInventory[position] = true; //free position
        --itemsCount;
    }

    public void AddCoins(int amount)
    {
        if (amount > 0)
        {
            coinsAmount += amount;
            textCoinsAmount.text = coinsAmount.ToString();
        }
    }

    public void SpendCoins(int amount, string item)
    {
		if (!IsInventoryFull()) {
			if (amount < 0 && coinsAmount >= (-1 * amount)) {
				coinsAmount += amount;
				textCoinsAmount.text = coinsAmount.ToString ();

				StartCoroutine (AddInventory (item));
			} else {
				StartCoroutine(FindObjectOfType<HUDAnnouncer>()
					.DisplayAnnounce("Can't buy - " + item + "\nHaven't enough coins.", 2f));
			}
		} else {
			StartCoroutine(FindObjectOfType<HUDAnnouncer>()
				.DisplayAnnounce("Can't buy - " + item + "\nInventory is full.", 2f));
		}
    }

	private bool IsInventoryFull()
	{
		return !(itemsCount <= maxItemCount);
	}

    //serch for free position in inventory
    private Vector3 GetFreePosition()
    {
        var freePosition = Vector3.zero;

        foreach (var item in freePositionsInInventory)
        {
            if (item.Value) //if position is free - return it
            {
                freePosition = item.Key;
                freePositionsInInventory[item.Key] = false;
                break;
            }

        }

        return freePosition;
    }
}
