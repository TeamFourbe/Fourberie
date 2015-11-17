using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScript : NetworkBehaviour {

    public int id;
    public int nbOfRessource;
    public Color playerColor;
    // Use this for initialization
    void Start () {
        nbOfRessource = 10;
        PlayerManager.instance.addPlayer(this);
        SetId(PlayerManager.instance.playerList.Count);
	}


    [Command]
    private void CmdSetPlayerId(GameObject go)
    {
        go.GetComponent<PlayerScript>().SetId(TurnManager.instance.playerIdList.Count+1);
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int Id)
    {
        id = Id;
    }
}
