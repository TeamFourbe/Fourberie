using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int id;
    public int nbOfRessource;
    public Color playerColor;
    // Use this for initialization
    void Start () {
        nbOfRessource = 10;
        PlayerManager.instance.addPlayer(this);
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
