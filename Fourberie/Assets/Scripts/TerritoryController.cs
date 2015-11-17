using UnityEngine;
using System.Collections;

public class TerritoryController : MonoBehaviour {

    public enum Etat
    {
        FREE,
        GOAL,
        CAPTURED,
    }

    public int idPlayer = 0;

    public int strength = 0;

    public Etat etat = Etat.FREE;

    public GameObject Region;
    
    void Start()
    {
        Region = transform.parent.gameObject;
    }

    public void SwitchEtat(Etat newEtat)
    {
        etat = newEtat;

        if(newEtat == Etat.GOAL)
        {
            //GetComponent<Renderer>().material.color = Color.cyan;
        }

        strength = 0;
    }

    public void SwitchEtat(Etat newEtat, int id)
    {
        etat = newEtat;

        if (newEtat == Etat.GOAL)
        {
            if(id == 1)
            {
                GetComponent<Renderer>().material.color = Color.cyan;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }

        idPlayer = id;
        strength = 1;
    }

    public bool IsNear(int id)
    {
       /*if(getNeighboor(0, 1).idPlayer == id || getNeighboor(0, -1).idPlayer == id || getNeighboor(1, 0).idPlayer == id || getNeighboor(-1, 0).idPlayer == id) {
            GetComponent<Renderer>().material.color = Color.red;
        }*/
        return getNeighboor(0,1).idPlayer == id || getNeighboor(0, -1).idPlayer == id || getNeighboor(1, 0).idPlayer == id || getNeighboor(-1, 0).idPlayer == id;
    }

    public TerritoryController getNeighboor(int x, int y)
    {

        if((int)transform.localPosition.x + x <= MapManager.Instance.width-1 && (int)transform.localPosition.y + y <= MapManager.Instance.length-1 && (int)transform.localPosition.y + y >= 0 && (int)transform.localPosition.x + x >= 0)
        {
            return MapManager.Instance.map[(int)transform.localPosition.x + x +((int)transform.localPosition.y + y)*MapManager.Instance.length].GetComponent<TerritoryController>();
        }
        else
        {
            return this;
        }
    }
}
