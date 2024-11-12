using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosswalksc : MonoBehaviour
{
    GameObject nesne;

    int x = 0;
    private void OnTriggerEnter(Collider other)
    {

        GameObject nesne = other.gameObject;
        if (nesne != null)
        {
            if (nesne.layer == 10)  
            {
                gameObject.layer = 7;
                ++x;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject nesne = other.gameObject;
        if (nesne.layer == 10) // Belirli bir Layer'deki nesneler için
        {
            --x;
            if (x==0)
            {
                gameObject.layer = 0;
            }
        }
    }





}
