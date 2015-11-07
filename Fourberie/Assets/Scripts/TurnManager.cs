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
    private int currentPlayer;

    // Use this for initialization
    void Start () {
        playerIdList = new List<int>();
        currentPlayer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Turn : " + currentPlayer);
	}

    public void Endturn()
    {
        currentPlayer++;
        currentPlayer = currentPlayer % playerIdList.Count;
        if(currentPlayer == 0)  
            EndOfGlobalTurn();
    }

    public void EndOfGlobalTurn()
    {
        Debug.Log("Fin de tour global");
    }
}
