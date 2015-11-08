using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class tileCursorTest : MonoBehaviour
{

    public Texture2D RenforceTexture;
    public Texture2D AttackTexture;
    public Texture2D ConquereTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;



    public void OnMouseOver()
    {

        TerritoryController territory = GetComponent<TerritoryController>();
        if(territory.etat == TerritoryController.Etat.FREE && territory.IsNear(TurnManager.instance.currentPlayer))
        {
            Cursor.SetCursor(ConquereTexture, hotSpot, cursorMode);
            if (Input.GetMouseButtonDown(0))
            {
                PlayerManager.instance.Conquer(TurnManager.instance.currentPlayer, gameObject);
            }
        }
        else if (territory.etat == TerritoryController.Etat.CAPTURED || territory.etat == TerritoryController.Etat.GOAL)
        {
            if(territory.idPlayer != TurnManager.instance.currentPlayer && territory.IsNear(TurnManager.instance.currentPlayer))
            {
                Cursor.SetCursor(AttackTexture, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    PlayerManager.instance.Attack(TurnManager.instance.currentPlayer, gameObject);
                }
            }  
            else if (territory.idPlayer == TurnManager.instance.currentPlayer)
            {
                Cursor.SetCursor(RenforceTexture, hotSpot, cursorMode);
                if (Input.GetMouseButtonDown(0))
                {
                    PlayerManager.instance.Reinforce(TurnManager.instance.currentPlayer, gameObject);
                }
            }

        }               
        
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
