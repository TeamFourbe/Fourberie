using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ManagerInitializer : NetworkBehaviour {

    public GameObject[] ListToSpawn;

	// Use this for initialization
	void Start () {
        if (isServer)
         foreach (GameObject obj in ListToSpawn)
            NetworkServer.Spawn(Instantiate(obj) as GameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
            Debug.Log("je suis local");
	}
}
