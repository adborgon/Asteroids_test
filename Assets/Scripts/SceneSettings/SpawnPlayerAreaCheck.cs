using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerAreaCheck : MonoBehaviour
{
    public bool objectInsideArea;

    private void OnTriggerStay2D(Collider2D collision)
    {
        objectInsideArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectInsideArea = false;
    }
}