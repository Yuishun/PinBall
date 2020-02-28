using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    uint combo = 0;
    public uint Combo
    {
        get { return combo; }
    }

    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ComboPlus()
    {
        if(++combo > 1)
            text.text = combo.ToString() + " combo";
    }

    public void ComboEnd()
    {
        combo = 0;
        text.text = "";
    }
}
