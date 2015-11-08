using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegionController : MonoBehaviour
{

    public List<GameObject> territories = new List<GameObject>();
    List<int> playerList = new List<int>();

    public int value;

    void Start()
    {
        foreach (Transform child in transform)
        {
            territories.Add(child.gameObject);
        }
        value = territories.Count / 2;
    }


    public void giveRessources()
    {
        if (territories.Count == 0)
        {
            return;
        }


        playerList.Clear();
        for(int i = 0;i<TurnManager.instance.playerIdList.Count;++i)
        {
            // on initialise les joueur a 0 territoire
            playerList.Add(0);
        }
        for (int i = 0; i < territories.Count; ++i)
        {
            if(territories[i].GetComponent<TerritoryController>().idPlayer == 0)
            {
                continue;
            }
            int playerID = territories[i].GetComponent<TerritoryController>().idPlayer - 1;
            playerList[playerID]++;
        }
        int bestPlayer = 0;
        int bestScore = 0;
        for (int i = 0; i < playerList.Count; ++i)
        {
            if(playerList[i] > bestScore)
            {
                bestPlayer = i+1;
                bestScore = playerList[i];
            }
        }
        if (bestScore >= (territories.Count / 2))
        {
            Debug.Log("distribut " + value + " to player " + bestPlayer);
            PlayerManager.instance.DistributeResources(bestPlayer, value);
        }

    }
}
