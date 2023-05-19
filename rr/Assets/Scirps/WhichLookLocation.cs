using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichLookLocation : MonoBehaviour
{
    public bool isLookingRight;
    public void RotatePlayerLocation(GameObject _gameObject)
    {
         if (Input.GetKeyDown(KeyCode.A))
         {
             _gameObject.transform.localScale = new Vector3(-1, 1, 1);
         }

         else if (Input.GetKeyDown(KeyCode.D))
         {
             _gameObject.transform.localScale = new Vector3(1, 1, 1);
         }
    }
    
}
