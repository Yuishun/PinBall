using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    [SerializeField]
    private GameObject m_poolObj;
    List<GameObject> m_TextpoolList;

    [SerializeField]
    private const int MAXTEXT = 10;


    private void Awake()
    {
        m_TextpoolList = new List<GameObject>();
        // Create Pool
        for (int i = 0; i < MAXTEXT; i++)
        {
            var newObj = CreateText();

            m_TextpoolList.Add(newObj);
        }
    }

    public GameObject GetText()
    {
        // 使用中でないものを探して返す
        foreach (var obj in m_TextpoolList)
        {
            var objrb = obj.GetComponent<GameText>();
            if (objrb.IsDraw == false)
            {
                objrb.IsDraw = true;                
                return obj;
            }
        }

        // 全て使用中だったら新しく作り、リストに追加してから返す
        var newObj = CreateText();
        m_TextpoolList.Add(newObj);

        return newObj;
    }

    private GameObject CreateText()
    {
        GameObject newObj = Instantiate(m_poolObj, new Vector2(10, 0),
            Quaternion.identity);
        newObj.name = m_poolObj.name + m_TextpoolList.Count + 1;
        newObj.transform.parent = this.transform;

        return newObj;
    }

}
