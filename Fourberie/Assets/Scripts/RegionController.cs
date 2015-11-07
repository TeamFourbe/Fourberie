using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegionController : MonoBehaviour {

    public List<GameObject> territories = new List<GameObject>();
    
    void Start()
    {
        foreach(Transform child in transform)
        {
            territories.Add(child.gameObject);
        }
    }

}
