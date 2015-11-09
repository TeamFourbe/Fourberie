using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MessageHandler : NetworkBehaviour {


    // http://answers.unity3d.com/questions/989015/unet-spawn-object-to-parent.html

	// Use this for initialization
	void Awake () {
        NetworkServer.RegisterHandler(Message.SetParent, OnSetParent);
	}

    private void OnSetParent(NetworkMessage netMsg)
    {
        SetParentMessage msg = netMsg.ReadMessage<SetParentMessage>();
        ClientScene.objects[msg.netId].transform.parent = ClientScene.objects[msg.parentNetId].transform;
    }
}
