using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Point : MonoBehaviour
{
    [HideInInspector]public EnemyContoller contoller;

    
    public GameObject EnemyObj;
    Enemy child;
    public int Hp,Pow;

    [SerializeField]
    ParticleSystem paSpawn, paDie;

    bool UpdateFlag = true;
    float fTime = 0;
    bool Coroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        EnemyObj = transform.GetChild(0).gameObject;
        child = EnemyObj.GetComponent<Enemy>();
        child.enemy.CharaReset(Hp, Pow);
    }

    // Update is called once per frame
    public void E_PointUpdate()
    {
        if (!Coroutine) { 
            if (UpdateFlag)
            {
                if (!child.E_Update())
                    StartCoroutine(Die());
            }
            else
            {
                fTime += Time.deltaTime;
                if (fTime >= 2f)
                {
                    StartCoroutine(Spawn());
                    fTime = 0;
                }
            }
        }
    }

    public IEnumerator Spawn()
    {
        Coroutine = true;

        yield return new WaitForSeconds(1f);
        UpdateFlag = true;
        EnemyObj.SetActive(true);
        child.enemy.CharaReset(Hp, Pow);

        Coroutine = false;
    }
    public IEnumerator Die()
    {
        Coroutine = true;
        
        yield return new WaitForSeconds(1f);
        UpdateFlag = false;
        contoller.EnemyCount++;
        EnemyObj.SetActive(false);


        Coroutine = false;
    }
}
