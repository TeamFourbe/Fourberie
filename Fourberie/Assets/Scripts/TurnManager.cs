using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Networking;

public class TurnManager : MonoBehaviour {

    public static TurnManager instance = null;
   
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<int> playerIdList;
    public int currentPlayer;

    // Use this for initialization
    void Start () {
        playerIdList = new List<int>();
        currentPlayer = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Turn : " + currentPlayer);
	}

    public void Endturn()
    {
        currentPlayer = currentPlayer % playerIdList.Count;
        currentPlayer++;
        if(currentPlayer == 1)  
            EndOfGlobalTurn();
    }

    public void EndOfGlobalTurn()
    {
        Debug.Log("fin de tour global");
        List<GameObject> regionList = MapManager.Instance.regionList;
        for (int i =0; i < regionList.Count; ++i)
        {
            regionList[i].GetComponent<RegionController>().giveRessources();
        }
        PlayerManager.instance.endRegen();
    }
}
