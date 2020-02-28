using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Point : MonoBehaviour
{
    [SerializeField]
    ParticleSystem parSpawn, parDeath;

    public BossEnemy Boss;

    bool UpdateFlag;
    [SerializeField] GameObject Clear,Combo,canvas;

    SoundManager2 sound;
    // Start is called before the first frame update
    void Start()
    {
        //Boss = GetComponentInChildren<BossEnemy>();
        UpdateFlag = false;
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdateFlag)
            return;

        if (!Boss.BE_Update())
        {
            StartCoroutine(Death());
            sound.PlayDeath();
        }
    }

    public IEnumerator Spawn()
    {
        parSpawn.Play();
        yield return new WaitForSeconds(1f);
        UpdateFlag = true;
        Boss.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        parSpawn.Stop();
        sound.PlaySpawn();
    }

    IEnumerator Death()
    {
        parDeath.Play();
        iTween.ColorTo(Boss.gameObject, iTween.Hash(
            "a", 0,
            "time", 2f,
            "easetype", iTween.EaseType.easeInQuart));
        UpdateFlag = false;
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
        Combo.SetActive(false);
        Clear.SetActive(true);
        Boss.gameObject.SetActive(false);
    }

}
