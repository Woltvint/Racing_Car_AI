using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool touch = false;

    private void OnTriggerEnter2D()
    {
        touch = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touch = false;
    }


}
