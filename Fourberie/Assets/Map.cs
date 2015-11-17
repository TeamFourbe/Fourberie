using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Map : NetworkBehaviour {


    public GameObject[] map;
    public List<GameObject> regionList;

    void Start()
    {
        GameObject[] tmpMap = GameObject.FindGameObjectsWithTag("Territory");
        map = new GameObject[tmpMap.Length];
        foreach(GameObject obj in tmpMap)
        {
            map[(int)obj.transform.position.x + (int)obj.transform.position.y * MapManager.Instance.length] = obj;
        }
        MapManager.Instance.map = map;
        
    }
}
