using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fTime;

    private RectTransform rctrance;

    public bool IsDraw;

    private void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        rctrance = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDraw)
            return;

        fTime += Time.deltaTime;
        if (fTime >= 2)
        {
            fTime = 0;
            IsDraw = false;
            rctrance.position = new Vector2(10, 0);
        }
    }

    public void DamageTextMove(int easetype,int damage)
    {
        IsDraw = true;
        text.text = damage.ToString();

        iTween.MoveBy(gameObject, iTween.Hash(
            "time", 0.5f,
            "x", 0.4f,
            "y", 0.1f,
            "easetype",(iTween.EaseType)easetype
            ));
    }
}
