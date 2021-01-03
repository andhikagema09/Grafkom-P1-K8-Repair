using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float crystal = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Collectibles")
        {
            Destroy(other.gameObject);
        }
    }
}
