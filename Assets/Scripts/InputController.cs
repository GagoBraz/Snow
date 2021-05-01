using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
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
            return Input.GetKeyDown(KeyCode.E);
        }
    }

}
