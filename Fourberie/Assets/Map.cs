using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Map : NetworkBehaviour {


    public GameObject[,] map;
    public List<GameObject> regionList;
}
