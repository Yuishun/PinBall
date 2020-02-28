using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripArea : MonoBehaviour
{
    [SerializeField]
    Fripper Frip;
    // Start is called before the first frame update
    void Start()
    {
        Frip = GetComponentInChildren<Fripper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Frip.Aim(collision.transform.position);
            Frip.OnFrip = true;
        }
    }
}
