using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Lava : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (playerStats != false)
        {
            playerStats.Die();
        }
        else
        {
            Destroy(collision.gameObject);
        }           
    }

}
