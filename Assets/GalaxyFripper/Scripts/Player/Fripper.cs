using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fripper : MonoBehaviour
{
    [SerializeField]
    Transform Point;
    Vector3 center = Vector3.zero;

    public bool OnFrip;

    [SerializeField]
    Ball ball;
    public Vector2 FripVec;

    Rigidbody2D Frip;

    public SpriteRenderer VecGuid;

    public Shot_Penetrating Shot;

    SoundManager2 sound;

    private void Start()
    {
        Point = transform.parent;
        OnFrip = false;
        Frip = gameObject.GetComponent<Rigidbody2D>();
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager2>();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Ball>().bFrip = true;
            Aim(collision.transform.position);
            OnFrip = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnFrip = false;
    }

    public void Rotate(float angle)
    {
        Frip.MoveRotation(angle);
    }
    public void Aim(Vector3 vector)
    {
        center = vector - Point.position;
        FripVec = (Vector2)center.normalized;
        VecGuid.transform.rotation =
            Quaternion.FromToRotation(Vector2.up, FripVec);
        VecGuid.transform.position = ball.transform.position;
    }
    public void Force(float force)
    {
        if (OnFrip)
        {
            uint combo = ball.combo.Combo;
            ball.combo.ComboEnd();
            FripVec.y = 1;
            //Debug.Log("Fripvec"+FripVec);
            ball.rigit.velocity = FripVec * force;
            if (combo >= 3)
            {
                Shot.rb2d.simulated = true;
                Shot.transform.position = ball.transform.position;
                Shot.transform.rotation =
                  Quaternion.FromToRotation(Vector2.up, FripVec);
                Shot.rb2d.velocity = FripVec * 16;
                Shot.power = ball.ball.Pow + (int)(combo / 2);
                sound.PlayChargeShot();
            }
            else sound.PlayFrip();
            OnFrip = false;
        }
    }
}
