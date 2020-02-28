using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public SpriteRenderer sprite;

    public int power;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (rb2d.simulated == false)
            return;

        if (!sprite.isVisible)
        {
            rb2d.simulated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Ball>().Damage(power);
            Delete(new Vector2(10, 5));
        }
    }

    public void Delete(Vector2 pos)
    {
        transform.position = pos;
        rb2d.simulated = false;
    }
}
