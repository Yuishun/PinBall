using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.position =
                transform.GetChild(0).position;
            collision.GetComponent<Rigidbody2D>().velocity =
                new Vector2(0, 0);
            collision.GetComponent<Ball>().ball.Damage(10,(int)DamageState.PLAYER_NORMAL);
        }
    }
}
