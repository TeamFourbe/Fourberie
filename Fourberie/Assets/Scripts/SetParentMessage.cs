using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Message
{
    // used for auto-increment
    private enum Type
    {
        SetParent = MsgType.Highest + 1
    }

    public const short SetParent = (short)Type.SetParent;
}

public class SetParentMessage : MessageBase
{
    public NetworkInstanceId netId;
    public NetworkInstanceId parentNetId;

    public SetParentMessage()
    {
    }
    public SetParentMessage(GameObject gameObject, GameObject parent)
    {
        netId = GetNetId(gameObject);
        parentNetId = GetNetId(parent);
    }

    private static NetworkInstanceId GetNetId(GameObject obj)
    {
        // NOTE: no error handling
        return obj.GetComponent<NetworkIdentity>().netId;
    }
}