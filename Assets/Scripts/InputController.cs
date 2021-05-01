using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public static float movingAxisX 
    { get
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }

    public static float movingAxisY
    {
        get
        {
            return Input.GetAxisRaw("Vertical");
        }
    }

    public static bool hidingControl
    {
        get
        {
            return Input.GetKey(KeyCode.E);
        }
    }

    public static bool hidingBarControl {
        get
        {
            return Input.GetMouseButton(0);
        }
    }


}
