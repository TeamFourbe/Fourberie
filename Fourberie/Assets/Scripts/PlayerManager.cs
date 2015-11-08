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
    public int regen;

    // Use this for initialization
    void Start () {
        playerList = new List<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void endRegen()
    {
        for (int i = 0;i< playerList.Count;++i)
        {
            playerList[i].nbOfRessource += regen;
        }
    }

    public void DistributeResources(int playerID, int nbOfResources)
    {
        Debug.Log("DistributeResources to " + playerID + " for " + nbOfResources);
        playerList[playerID - 1].nbOfRessource += nbOfResources;
    }

    public void addPlayer(PlayerScript player)
    {
        playerList.Add(player);
        player.playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void InstanciatePlayer()
    {
        GameObject instance = Instantiate(prefab) as GameObject;
        instance.GetComponent<PlayerScript>().SetId(playerList.Count+1);
        TurnManager.instance.playerIdList.Add(playerList.Count+1);
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

    public void Conquer(int playerID, GameObject territory)
    {
        if (playerList[playerID - 1].nbOfRessource <= 0)
        {
            TurnManager.instance.Endturn();
        }
        else
        {
            TerritoryController tc = territory.GetComponent<TerritoryController>();
            tc.gameObject.GetComponent<Renderer>().material.color = playerList[playerID - 1].playerColor;
            tc.SwitchEtat(TerritoryController.Etat.CAPTURED, playerID);
            playerList[playerID - 1].nbOfRessource--;
        }
        
    }

    public void Attack(int playerID, GameObject territory)
    {
        if (playerList[playerID - 1].nbOfRessource <= 0)
        {
            TurnManager.instance.Endturn();
        }
        else
        {
            TerritoryController tc = territory.GetComponent<TerritoryController>();
            tc.strength--;
            playerList[playerID - 1].nbOfRessource--;
            if(tc.strength <= 0)
            {
                tc.gameObject.GetComponent<Renderer>().material.color = Color.white;
                tc.SwitchEtat(TerritoryController.Etat.FREE, 0);
            }
        }
        Debug.Log("je attaque" + playerID);
    }

    public void Reinforce(int playerID, GameObject territory)
    {
        if (playerList[playerID - 1].nbOfRessource <= 0)
        {
            TurnManager.instance.Endturn();
        }
        else
        {
            TerritoryController tc = territory.GetComponent<TerritoryController>();
            tc.strength++;
            playerList[playerID - 1].nbOfRessource--;
        }
        Debug.Log("je renforce" + playerID);
    }
}
