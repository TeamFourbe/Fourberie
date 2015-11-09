﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MessageHandler : NetworkBehaviour {


    // http://answers.unity3d.com/questions/989015/unet-spawn-object-to-parent.html

	// Use this for initialization
	public override void OnStartServer () {
        NetworkServer.RegisterHandler(Message.SetParent, OnSetParent);
	}

    private void OnSetParent(NetworkMessage netMsg)
    {
        Debug.Log("lol");
        SetParentMessage msg = netMsg.ReadMessage<SetParentMessage>();
        ClientScene.objects[msg.netId].transform.parent = ClientScene.objects[msg.parentNetId].transform;
    }

}
