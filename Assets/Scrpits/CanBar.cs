using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBar : MonoBehaviour
{
    // KALKANIDA BURADA KONTROL ETTIM 
    public GameObject canbar;
    public static float can { get; set; }
    private bool shieldIsActive;
    /*
     * if (collisen.gameobject.tag == "enemynin attigi yada vurdugu durumun tag i yazilcak")
     * {
     *     if(shildIsActive! ) // kalkan kapaliysa
     *     {
     *        can -= enemynin vurdugu hasar
     *     } 
     * 
     * }
     * 
     * 
     * 
     * 
     * 
     */
    // Start is called before the first frame update
    void Start()
    {
        can = 100;
    }

    // Update is called once per frame
    void Update()
    {
        canbar.transform.localScale = new Vector3(can / 100, 1, 1); // can barinin hareketini burada kontrol ediyorum
        if (can >= 100)
        {
            can = 100;
        }
        if (can <= 0)
        {
            can = 0;
        }

    }
}
