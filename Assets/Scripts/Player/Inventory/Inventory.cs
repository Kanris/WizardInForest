using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private GameObject inventory;
    private bool isActive = false;

    private List<GameObject> list;
    private Dictionary<Vector3, bool> freePositionsInInventory;


	// Use this for initialization
	void Start () {

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(isActive);

        list = new List<GameObject>(3);

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

    public IEnumerator AddInventory(string item)
    {
        if (list.Count < list.Capacity)
        {
            var newItem = Instantiate(Resources.Load<GameObject>("Prefab/Inventory/" + item));
            newItem.transform.SetParent(inventory.transform);
            newItem.GetComponent<RectTransform>().localPosition = GetFreePosition();

            list.Add(newItem);
        }
        else
        {
            GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>().text = "Inventory is full";

            yield return new WaitForSeconds(1f);

            GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>().text = string.Empty;
        }
    }

    public void ItemUse()
    {
        for(int index = 0; index < list.Count; index++)
        {
            if (!list[index].active)
            {
                Destroy(list[index]);
                list.RemoveAt(index);
            }
        }
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
