using UnityEngine;
using System.Collections;

public class tileCursorTest : MonoBehaviour {

    public Texture2D RenforceTexture;
    public Texture2D AttackTexture;
    public Texture2D ConquereTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void OnMouseEnter()
    {
        switch(gameObject.tag)
        {
            case "empty":
                Cursor.SetCursor(ConquereTexture, hotSpot, cursorMode);
                break;
            case "opponent":
                Cursor.SetCursor(AttackTexture, hotSpot, cursorMode);
                break;
            case "friendly":

                Cursor.SetCursor(RenforceTexture, hotSpot, cursorMode);
                break;
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    
}
