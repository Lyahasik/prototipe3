using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastVector : MonoBehaviour
{
    static public Vector3 CastToVector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
    
    static public Vector2 CastToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
}
