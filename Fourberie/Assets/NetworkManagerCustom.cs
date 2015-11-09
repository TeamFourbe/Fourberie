using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkManagerCustom : NetworkManager {

    public override void OnStartServer()
    {
        Debug.Log("serverStart");
        NetworkServer.RegisterHandler(Message.SetParent, OnSetParent);
    }

    private void OnSetParent(NetworkMessage netMsg)
    {
        SetParentMessage msg = netMsg.ReadMessage<SetParentMessage>();
        ClientScene.objects[msg.netId].transform.parent = ClientScene.objects[msg.parentNetId].transform;
    }

}
