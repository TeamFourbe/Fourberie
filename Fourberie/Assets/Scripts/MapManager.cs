using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

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

    public GameObject[,] map;
    public List<GameObject> regionList;

    public int width = 50;
    public int length = 10;

    public int nbRegion = 10;

	// Use this for initialization

    void Awake()
    {
        instance = this;

        regionList = new List<GameObject>();
        map = new GameObject[width, length];

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Territory"))
        {
            map[(int)go.transform.localPosition.x, (int)go.transform.localPosition.y] = go;
        }
        

        /*
        GameObject mapParent = new GameObject();
        mapParent.name = "Map";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject go = Instantiate(territoryPrefab, new Vector2(i, j), Quaternion.identity) as GameObject;
                go.transform.parent = mapParent.transform;
                map[i, j] = go;

                if (i == 0 && j == (int)(length / 2))
                {
                    go.GetComponent<TerritoryController>().SwitchEtat(TerritoryController.Etat.GOAL);
                }
                else if (i == width - 1 && j == (int)(length / 2))
                {
                    go.GetComponent<TerritoryController>().SwitchEtat(TerritoryController.Etat.GOAL);
                }

            }
        }*/
    }

    void CreateRegions()
    {
        RegionDimension = width * length / nbRegion;

        /*for(int i= 0; i<nbRegion/2; i++)
        {
            GameObject region = new GameObject();
            List<GameObject> regionTerritories = new List<GameObject>();

            for(int j=0; j<RegionDimension; j++)
            {
                if(regionTerritories.Count == 0)
                {
                    regionTerritories.Add(map[,])
                }
            }
        }*/

        /*GameObject region = new GameObject();
        List<GameObject> regionTerritories = new List<GameObject>();

        Color c = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if(!map[i,j].GetComponent<TerritoryController>().Region)
                {
                    map[i, j].GetComponent<TerritoryController>().Region = region;
                    map[i, j].GetComponent<Renderer>().material.color = c;
                    regionTerritories.Add(map[i, j]);
                }
            }
        }*/

        for (int i = 0; i < nbRegion / 2; i++)
        {
            regionList.Add(NewRegion());
        }

    }

    GameObject NewRegion()
    {
        GameObject region = new GameObject();
        List<GameObject> regionTerritories = new List<GameObject>();

        Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        while(regionTerritories.Count < RegionDimension)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (!map[i, j].GetComponent<TerritoryController>().Region && regionTerritories.Count < RegionDimension)
                    {
                        map[i, j].GetComponent<TerritoryController>().Region = region;
                        map[i, j].GetComponent<Renderer>().material.color = c;
                        regionTerritories.Add(map[i, j]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return region;
    }

    void SetFrontier(GameObject territory, WallPosition position)
    {

        GameObject wall;

        switch (position)
        {
            case WallPosition.BOTTOM:
                wall = Instantiate(wallPrefab,new Vector3(territory.transform.position.x, territory.transform.position.y - 0.5f,-1),Quaternion.identity) as GameObject;
                break;
            case WallPosition.TOP:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x, territory.transform.position.y + 0.5f,-1), Quaternion.identity) as GameObject;
                break;
            case WallPosition.RIGHT:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x + 0.5f, territory.transform.position.y,-1), Quaternion.identity) as GameObject;
                wall.transform.Rotate(0,0,90);
                break;
            case WallPosition.LEFT:
                wall = Instantiate(wallPrefab, new Vector3(territory.transform.position.x - 0.5f, territory.transform.position.y,-1), Quaternion.identity) as GameObject;
                wall.transform.Rotate(0, 0, 90);
                break;
        }
    }
}
