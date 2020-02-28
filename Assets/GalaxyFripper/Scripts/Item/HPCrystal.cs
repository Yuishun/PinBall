using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCrystal : MonoBehaviour
{

    SoundManager2 sound;

    private void Awake()
    {
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Ball>().ball.HP += 20;
            sound.PlayCure();
            this.gameObject.SetActive(false);
        }
    }

}
