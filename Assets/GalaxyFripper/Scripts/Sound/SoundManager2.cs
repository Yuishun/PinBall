using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2 : MonoBehaviour
{
    public AudioClip Frip;
    public AudioClip Charge;
    public AudioClip ComboShot;
    public AudioClip BulletShot;
    public AudioClip LockVec;
    public AudioClip Cure;
    public AudioClip Damage;
    public AudioClip Spawn;
    public AudioClip Death;

    AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayFrip()
    {
        audio.PlayOneShot(Frip);
    }
    public void PlayCharge()
    {
        audio.PlayOneShot(Charge);
    }
    public void PlayChargeShot()
    {
        audio.PlayOneShot(ComboShot);
    }
    public void PlayBullet()
    {
        audio.PlayOneShot(BulletShot);
    }
    public void PlayLockVec()
    {
        audio.PlayOneShot(LockVec);
    }
    public void PlayCure()
    {
        audio.PlayOneShot(Cure);
    }
    public void PlayDamage()
    {
        audio.PlayOneShot(Damage);
    }
    public void PlaySpawn()
    {
        audio.PlayOneShot(Spawn);
    }
    public void PlayDeath()
    {
        audio.PlayOneShot(Death);
    }
}
