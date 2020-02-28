using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Ball Chara;

    [SerializeField]
    Fripper Right;
    public Fripper right
    {
        get { return Right; }
    }

    [SerializeField]
    Fripper Left;
    public Fripper left
    {
        get { return Left; }
    }

    [SerializeField]
    float FripperForce, LockMag;

    [SerializeField]
    SpriteRenderer VecGuid;

    [SerializeField]
    Shot_Penetrating Shot;

    // Start is called before the first frame update
    void Start()
    {
        Left.VecGuid = VecGuid;
        Right.VecGuid = VecGuid;
        Left.Shot = Shot;
        Right.Shot = Shot;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Right.Rotate(-15);
            Left.Rotate(15);
            if (!Right.OnFrip && !Left.OnFrip)
            {
                if (Chara.fGround == 99)
                    Chara.LockAddForce(LockMag);
            }
            Right.Force( FripperForce);
            Left.Force(FripperForce);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Right.Rotate(15);
            Left.Rotate(-15);
        }

        if (!Right.OnFrip && !Left.OnFrip)
        {
            VecGuid.transform.position = new Vector2(10, -5);
        }
    }
}
