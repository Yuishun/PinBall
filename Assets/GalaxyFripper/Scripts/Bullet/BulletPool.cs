using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private GameObject m_poolObj;
    List<GameObject> m_BulletpoolList;

    [SerializeField]
    private const int MAXBULLET = 10;

    public float Bulletspeed;

    SoundManager2 sound;

    private void Awake()
    {
        m_BulletpoolList = new List<GameObject>();
        // Create Pool
        for(int i = 0; i < MAXBULLET; i++)
        {
            var newObj = CreateBullet();
            newObj.GetComponent<Rigidbody2D>().simulated = false;
            m_BulletpoolList.Add(newObj);
        }
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager2>();
    }

    public GameObject GetBullet()
    {
        // 使用中でないものを探して返す
        foreach (var obj in m_BulletpoolList)
        {
            var objrb = obj.GetComponent<Rigidbody2D>();
            if (objrb.simulated == false)
            {
                objrb.simulated = true;
                return obj;
            }
        }

        // 全て使用中だったら新しく作り、リストに追加してから返す
        var newObj = CreateBullet();
        m_BulletpoolList.Add(newObj);

        newObj.GetComponent<Rigidbody2D>().simulated = true;
        return newObj;
    }

    private GameObject CreateBullet()
    {
        GameObject newObj = Instantiate(m_poolObj, new Vector2(10, 5),
            Quaternion.identity);
        newObj.name = m_poolObj.name + m_BulletpoolList.Count + 1;
        //newObj.GetComponent<Bullet>().Init();

        return newObj;
    }

    public void Shot(GameObject bullet,Vector2 vec2,int pow)
    {
        bullet.GetComponent<Rigidbody2D>().velocity = vec2 * Bulletspeed;
        bullet.GetComponent<Bullet>().power = pow;
        sound.PlayBullet();
    }
}
