using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    void Update()
    {
        Cursor.visible = false;
        transform.position = Input.mousePosition;
    }
}
