using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{

    public Charactor enemy;
    

    public BulletPool Pool;
    float fTime = 0;

    public float Degree, Angle_Split;
    float _theta;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = gameObject.AddComponent<Charactor>();
        enemy.CharaReset(30, 5);
        Pool = GameObject.Find("BulletPool_Normal").GetComponent<BulletPool>();
    }

    // Update is called once per frame
    public bool BE_Update()
    {
        if (enemy.HP <= 0)
        {
            fTime = 0;

            return false;
        }

        fTime += Time.deltaTime;
        if (fTime >= 5)
        {
            fTime = 0;
            Vector3 pos = transform.position;
            pos.y -= 1.1f;
            for (int i = 0; i <= (Angle_Split - 1); i++)
            {
                //n-way弾の端から端までの角度
                float AngleRange = Mathf.PI * (Degree / 180);

                //弾インスタンスに渡す角度の計算
                if (Angle_Split > 1) _theta = (AngleRange / (Angle_Split - 1)) * i + 0.5f * (Mathf.PI - AngleRange);
                else _theta = 0.5f * Mathf.PI;
                var Obj = Pool.GetBullet();
                Obj.transform.position = pos;

                Pool.Shot(Obj, new Vector2(Mathf.Cos(_theta), -Mathf.Sin(_theta)), enemy.Pow);
            }
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int pow = collision.transform.GetComponent<Ball>().ball.Pow;
            enemy.Damage(pow, (int)DamageState.ENEMY_NORMAL);
        }
    }

}
