using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {


    public static PlayerManager instance = null;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<PlayerScript> playerList;

    public GameObject prefab;

    // Use this for initialization
    void Start () {
        playerList = new List<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DistributeResources(int playerId, int nbOfResources)
    {

    }

    public void addPlayer(PlayerScript player)
    {
        playerList.Add(player);
    }

    public void InstanciatePlayer()
    {
        GameObject instance = Instantiate(prefab) as GameObject;
        instance.GetComponent<PlayerScript>().SetId(playerList.Count);
        TurnManager.instance.playerIdList.Add(playerList.Count);
    }

    public void useRessource(int playerId)
    {
        for(int i =0; i < playerList.Count; ++i)
        {
            if(playerId == playerList[i].GetId())
            {
                if(playerList[i].nbOfRessource > 0)
                {
                    playerList[i].nbOfRessource--;
                }
                else
                {
                    Debug.Log("T'es null");
                }
            }
        }
    }
}
