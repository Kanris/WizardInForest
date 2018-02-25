using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLevel : MonoBehaviour {

    public string SortingLayer = "Default";
    public int OrderInLayer = 0;

	// Use this for initialization
	void Start () {

        var mesh = GetComponent<MeshRenderer>();

        mesh.sortingLayerName = SortingLayer;
        mesh.sortingOrder = OrderInLayer;
	}
}
