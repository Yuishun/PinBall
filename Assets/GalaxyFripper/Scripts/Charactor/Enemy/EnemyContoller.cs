using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    Enemy_Point[] ESpawn;

    public uint EnemyCount = 0;    // 倒された数
    [SerializeField] uint EnemyQuota = 10;    // ノルマ

    [SerializeField] Boss_Point Boss;

    public void E_Init(uint count,uint quota)
    {
        EnemyCount = count;
        EnemyQuota = quota;
    }
    // Start is called before the first frame update
    void Start()
    {
        ESpawn = new Enemy_Point[transform.childCount];
        int i = 0;
        foreach(Transform child in transform)
        {
            ESpawn[i] = child.GetComponent<Enemy_Point>();
            ESpawn[i].contoller = this;
            i++;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(Enemy_Point epoint in ESpawn)
        {
            epoint.E_PointUpdate();
        }

        if (EnemyCount >= EnemyQuota)
        {
            StageClear();
            Boss.StartCoroutine(Boss.Spawn());
            this.enabled = false;
        }
    }

    void StageClear()
    {
        for(int i = 0; i < ESpawn.Length; i++)
        {
            ESpawn[i].EnemyObj.SetActive(false);
        }
    }
}
