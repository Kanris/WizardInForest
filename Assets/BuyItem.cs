using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour {

    [SerializeField]
    private int Price = 0;

    [SerializeField]
    private string Item = "Empty";

    [SerializeField]
    private GameObject inventory;

    public void Buy()
    {
        FindObjectOfType<Inventory>().SpendCoins(Price, Item);
    }

    private void OnMouseDown()
    {
        Buy();
    }
}
