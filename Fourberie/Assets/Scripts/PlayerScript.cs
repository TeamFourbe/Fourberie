﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int id;
    public int nbOfRessource;
    // Use this for initialization
    void Start () {
        nbOfRessource = 10;
        PlayerManager.instance.addPlayer(this);
	}
	
	// Update is called once per frame
	void Update () {
	
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
