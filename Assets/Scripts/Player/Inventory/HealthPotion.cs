using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IItem {

    [SerializeField]
    private int HealthCount = 1;

    public void Use()
    {
        FindObjectOfType<PlayerStats>().ManageHealth(HealthCount);
        FindObjectOfType<Inventory>().ItemUse(gameObject.GetComponent<RectTransform>().localPosition);
        Destroy(gameObject);
    }


}
