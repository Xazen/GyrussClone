using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Player : GameComponent<InGame>
{
    public void Start ()
    {
        // Setup delegate methods
        this.Game.gameInput.KeyDown += KeyDown;
        this.Game.gameInput.KeyUp += KeyUp;
    }

    #region GameInputDelegate
    public void KeyDown(string keyMappingName)
    {
        KeyCode keyCode = this.Game.gameInput.GetKeyMapping(keyMappingName).keyCode;

        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                Debug.Log("Pressed left");
                break;
            case KeyCode.RightArrow:
                Debug.Log("Pressed right");
                break;
            case KeyCode.Space:
                Debug.Log("Pressed space");
                break;
            default:
                break;
        }
    }

    public void KeyUp(string keyMappingName)
    {
        KeyCode keyCode = this.Game.gameInput.GetKeyMapping(keyMappingName).keyCode;

        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                Debug.Log("Released left");
                break;
            case KeyCode.RightArrow:
                Debug.Log("Released right");
                break;
            case KeyCode.Space:
                Debug.Log("Released space");
                break;
            default:
                break;
        }
    }
    #endregion
}

