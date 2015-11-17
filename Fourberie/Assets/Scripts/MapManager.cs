using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MapManager : NetworkBehaviour
{

    static MapManager instance;

    public static MapManager Instance
    {
        get
        {
            return instance;
        }
    }

    public enum WallPosition
    {
        TOP,
        BOTTOM,
        RIGHT,
        LEFT,
    }

    int RegionDimension;

    public GameObject territoryPrefab;
    public GameObject wallPrefab;
    public GameObject mapContainer;

    public GameObject[] mapList;
    public GameObject[] map;
    public GameObject[,] tmpMap;
    public List<GameObject> regionList;

    public int width = 50;
    public int length = 10;

    public int nbRegionMax = 10;


    // Use this for initialization

    GameObject mapParent;
    GameObject frontiersParent;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(gameObject);
    }

    public override void OnStartServer()
    {
        if (hasAuthority)
        {
            CmdGenerateBoard();
            Debug.Log("j'ai la puissance");
        }
    }
    void Start()
    {
        if(!hasAuthority)
        {
            getMap();
        }
    }

    void getMap()
    {
        Debug.Log("je get la map");
    }

    [ClientRpc]
    public void RpcSyncParent(GameObject child, GameObject parent)
    {
        Debug.Log("setParent");
         child.transform.parent = parent.transform;
    }

    [Command]
    void CmdGenerateBoard()
    {
        regionList = new List<GameObject>();
        tmpMap = new GameObject[width, length];
        GameObject currentMap = mapList[Random.Range(0, mapList.Length)];
        mapParent = Instantiate(currentMap) as GameObject;
        //mapParent = Instantiate(mapContainer) as GameObject;
        mapParent.name = "Map";
        /*
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject go = Instantiate(territoryPrefab, new Vector2(i, j), Quaternion.identity) as GameObject;
                //NetworkServer.Spawn(go);
                tmpMap[i, j] = go;

                if (i == 0 && j == (int)(length / 2))
                {
                    go.GetComponent<TerritoryController>().SwitchEtat(TerritoryController.Etat.GOAL, 1);
                }
                else if (i == width - 1 && j == (int)(length / 2))
                {
                    go.GetComponent<TerritoryController>().SwitchEtat(TerritoryController.Etat.GOAL, 2);
                }

            }
        }

        CreateRegions();
        map = new GameObject[width * length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                map[i + j * length] = tmpMap[i, j];
            }
        }*/
        mapParent.GetComponent<Map>().map = map;
        mapParent.GetComponent<Map>().regionList = regionList;
        NetworkServer.Spawn(mapParent);
    }

    private static void LinkToParent(GameObject child, GameObject parent)
    {
        child.transform.parent = parent.transform;
        NetworkServer.SendToAll(Message.SetParent, new SetParentMessage(child, parent));
    }

    void CreateRegions()
    {
        for (int i = 0; i < nbRegionMax / 2; i++)
        {
            regionList.Add(NewRegion(i + 1));
        }

        for (int i = 0; i < nbRegionMax / 2; i++)
        {
            CompleteRegions();
        }

        for (int i = 0; i < nbRegionMax / 2; i++)
        {
            ReplicateRegion(regionList[i]);
        }

        CreateFrontiers();
    }

    void CreateFrontiers()
    {

        frontiersParent = new GameObject();
        frontiersParent.name = "Frontieres";
        frontiersParent.transform.parent = mapParent.transform;
        frontiersParent.AddComponent<NetworkIdentity>();
        //NetworkServer.Spawn(frontiersParent);
        //RpcSyncParent(frontiersParent, mapParent);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                TerritoryController tc = tmpMap[i, j].GetComponent<TerritoryController>();

                if (tc.getNeighboor(0, 1).GetComponent<TerritoryController>().Region != tc.Region || tc.getNeighboor(0, 1) == tmpMap[i, j] || tmpMap[i, j].transform.localPosition.y == length - 1)
                {
                    SetFrontier(tmpMap[i, j], WallPosition.TOP);
                }
                if (tc.getNeighboor(-1, 0).GetComponent<TerritoryController>().Region != tc.Region || tmpMap[i, j].transform.localPosition.x == 0)
                {
                    SetFrontier(tmpMap[i, j], WallPosition.LEFT);
                }
                if (tmpMap[i, j].transform.localPosition.x == width - 1)
                {
                    SetFrontier(tmpMap[i, j], WallPosition.RIGHT);
                }
                if (tmpMap[i, j].transform.localPosition.y == 0)
                {
                    SetFrontier(tmpMap[i, j], WallPosition.BOTTOM);
                }
            }
        }
    }

    void ReplicateRegion(GameObject r)
    {
        GameObject region = new GameObject();
        region.name = r.name + " bis";
        region.transform.parent = mapParent.transform;
        region.AddComponent<NetworkIdentity>();
       // NetworkServer.Spawn(region);
        //RpcSyncParent(region, mapParent);

        for (int i = 0; i < width / 2; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (tmpMap[i, j].GetComponent<TerritoryController>().Region == r)
                {
                    tmpMap[width - i - 1, j].GetComponent<TerritoryController>().Region = region;
                    tmpMap[width - i - 1, j].transform.parent = region.transform;
                    //NetworkServer.Spawn(map[width - i - 1, j]);
                    //RpcSyncParent(map[width - i - 1, j], region);
                }
            }
        }

        region.AddComponent<RegionController>();
        regionList.Add(region);
    }

    void CompleteRegions()
    {
        for (int i = 0; i < width / 2; i++)
        {
            for (int j = 0; j < length; j++)
            {
                TerritoryController tc = tmpMap[i, j].GetComponent<TerritoryController>();

                if (!tc.Region)
                {

                    TerritoryController otherTc;

                    if (tc.getNeighboor(0, 1).Region && Random.Range(0f, 1f) > 0.5f)
                    {
                        otherTc = tc.getNeighboor(0, 1);
                    }
                    else if (tc.getNeighboor(0, -1).Region && Random.Range(0f, 1f) > 0.5f)
                    {
                        otherTc = tc.getNeighboor(0, -1);
                    }
                    else if (tc.getNeighboor(1, 0).Region && Random.Range(0f, 1f) > 0.5f)
                    {
                        otherTc = tc.getNeighboor(1, 0);
                    }
                    else
                    {
                        otherTc = tc.getNeighboor(-1, 0);
                    }

                    tc.Region = otherTc.Region;
                    if (otherTc.Region)
                    {
                        tmpMap[i, j].transform.parent = otherTc.Region.transform;
                    }
                }
            }
        }
    }

    GameObject NewRegion(int nbRegion)
    {
        GameObject region = new GameObject();
        region.name = "region " + nbRegion;
        region.transform.parent = mapParent.transform;
        region.AddComponent<NetworkIdentity>();
       //NetworkServer.Spawn(region);
        //RpcSyncParent(region, mapParent);

        int x = Random.Range(1, width / 2 - 1);
        int y = Random.Range(1, length - 1);

        for (int ix = -1; ix <= 1; ix++)
        {
            for (int iy = -1; iy <= 1; iy++)
            {
                tmpMap[x + ix, y + iy].GetComponent<TerritoryController>().Region = region;
                tmpMap[x + ix, y + iy].transform.parent = region.transform;
                //NetworkServer.Spawn(map[x + ix, y + iy]);
                //RpcSyncParent(map[x + ix, y + iy], region);
            }
        }

        region.AddComponent<RegionController>();
        return region;
    }

    void SetFrontier(GameObject territory, WallPosition position)
    {

        GameObject wall = null;

        switch (position)
        {
            case WallPosition.BOTTOM:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x, territory.transform.position.y - 0.5f, -1), Quaternion.identity) as GameObject;
                break;
            case WallPosition.TOP:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x, territory.transform.position.y + 0.5f, -1), Quaternion.identity) as GameObject;
                break;
            case WallPosition.RIGHT:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x + 0.5f, territory.transform.position.y, -1), Quaternion.identity) as GameObject;
                wall.transform.Rotate(0, 0, 90);
                break;
            case WallPosition.LEFT:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x - 0.5f, territory.transform.position.y, -1), Quaternion.identity) as GameObject;
                wall.transform.Rotate(0, 0, 90);
                break;
        }
        wall.transform.parent = frontiersParent.transform;
        //NetworkServer.Spawn(wall);
        //RpcSyncParent(wall, frontiersParent);
    }
}
