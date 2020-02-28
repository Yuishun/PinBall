using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    static int hashSpeed = Animator.StringToHash("Speed");
    static int hashFallSpeed = Animator.StringToHash("FallSpeed");
    static int hashGroundDistance = Animator.StringToHash("GroundDistance");
    static int hashIsCrouch = Animator.StringToHash("IsCrouch");

    static int hashDamage = Animator.StringToHash("Damage");

    public Charactor ball;
    
    public ComboManager combo;

    [SerializeField] LayerMask groundMask;

    public Rigidbody2D rigit;
    SpriteRenderer sprite;
    [SerializeField, HideInInspector] Animator animator;

    public float fGround;
     Vector2 LockVec;
    [SerializeField] SpriteRenderer LockGuid;
    float fTime = 0;

    SoundManager2 sound;
    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject.AddComponent<Charactor>();
        rigit = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        ball.CharaReset(100, 4);
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigit.velocity;
        if(velocity.x != 0)
            sprite.flipX = velocity.x < 0;

        var distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, 1, groundMask);

        fGround = distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - 0.21f;
        // ロックオンダッシュ
        if (fGround <= 0.1f)
            fTime = 0;
        if (fGround == 99 && fTime == 0)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 2.1f,
               ~LayerMask.GetMask("Stage", "Fripper", "Ally","Bullet"));
            if (col.Length != 0)
            {
                Vector2 mincol = Vector2.zero;
                float mindis = 99;
                foreach (Collider2D col2 in col)
                {
                    float dis =
                        Vector2.Distance(transform.position, col2.transform.position);
                    if (dis < mindis)
                    {
                        mindis = dis;
                        mincol = col2.transform.position;
                    }
                }
                LockVec = mincol - (Vector2)transform.position;
            }
            else LockVec = Vector2.zero;
        }
        else
        {
            LockVec = Vector2.zero;
        }
        if (LockVec != Vector2.zero)
        {
            LockGuid.enabled = true;
            LockGuid.transform.rotation =
                Quaternion.FromToRotation(Vector3.up, LockVec);
        }
        else
        {
            LockGuid.enabled = false;
        }
        if (fTime != 0)
        {
            fTime += Time.deltaTime;
            if (fTime >= 2f)
                fTime = 0;
        }

        animator.SetFloat(hashGroundDistance, fGround);
        animator.SetFloat(hashFallSpeed, velocity.y);
        animator.SetFloat(hashSpeed, Mathf.Abs(velocity.x));
    }

    public void Damage(int damage)
    {
        int dmState = (int)DamageState.PLAYER_NORMAL;
        if (rigit.velocity.y >= 3)
        {
            damage /= 3;
            dmState = (int)DamageState.PLAYER_GARD;
        }
        ball.Damage(damage,dmState);
        if (ball.HP <= 0)
            Debug.Log("Death");
        animator.SetTrigger(hashDamage);
    }

    public void LockAddForce(float mag)
    {
        if (LockVec == Vector2.zero)
            return;
        Debug.Log("LockAddForce"+LockVec.normalized);
        //rigit.AddForce(LockVec.normalized * mag, ForceMode2D.Impulse);
        rigit.velocity = LockVec.normalized * mag;
        LockVec = Vector2.zero;
        fTime += 0.001f;
        sound.PlayLockVec();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            combo.ComboPlus();
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        bFrip = false;
    }*/

}
