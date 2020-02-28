using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Charactor enemy ;

    public BulletPool Pool;
    float fTime = 0;

    SoundManager2 sound;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = gameObject.AddComponent<Charactor>();
        Pool = GameObject.Find("BulletPool_Normal").GetComponent<BulletPool>();
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager2>();
    }

    // Update is called once per frame
    public bool E_Update()
    {
        if (enemy.HP <= 0)
        {
            fTime = 0;
            return false;
        }
        if (Input.GetKeyDown(KeyCode.L)) 
            Debug.Log(enemy.HP + this.name);
        fTime += Time.deltaTime;
        if (fTime >= 5)
        {
            fTime = 0;
            var Obj = Pool.GetBullet();
            Obj.transform.position = transform.position;
            Pool.Shot(Obj, new Vector2(0, -1), enemy.Pow);
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int pow = collision.transform.GetComponent<Ball>().ball.Pow;
            enemy.Damage(pow,(int)DamageState.ENEMY_NORMAL);
            sound.PlayDamage();
        }
    }
}
