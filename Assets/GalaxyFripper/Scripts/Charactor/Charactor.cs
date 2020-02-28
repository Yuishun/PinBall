using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DamageState
{
    PLAYER_NORMAL,
    PLAYER_GARD,
    ENEMY_NORMAL,
    ENEMY_BREAK,
    ENEMY_WEAK,
}

public class Charactor : MonoBehaviour
{
    public int HP;      // 体力
    public int Pow;     // 攻撃力

    TextPool UItext;
    private void Start()
    {
        UItext = GameObject.Find("Canvas").GetComponent<TextPool>();
    }


    public void CharaReset(int hp,int pow)
    {       
        HP = hp;
        Pow = pow;
    }

    public void Damage(int damage,int dmState)
    {
        HP -= damage;
        var obj = UItext.GetText();
        obj.GetComponent<RectTransform>().position = this.transform.position;
        var gtext = obj.GetComponent<GameText>();
        int easetype = 0;
        // easetype 決め
        switch ((DamageState)dmState)
        {
            case DamageState.PLAYER_NORMAL:
            case DamageState.PLAYER_GARD:
                easetype = (int)iTween.EaseType.linear;
                break;
            case DamageState.ENEMY_NORMAL:
                easetype = (int)iTween.EaseType.spring;
                break;
        }
        // text color 決め
        switch ((DamageState)dmState)
        {
            case DamageState.PLAYER_NORMAL:
                gtext.text.color = Color.red;
                break;
            case DamageState.PLAYER_GARD:
                gtext.text.color = Color.blue;
                break;
            default:
                gtext.text.color = Color.white;
                break;
        }

        gtext.DamageTextMove(easetype, damage);
    }
}
