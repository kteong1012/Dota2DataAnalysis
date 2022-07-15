using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoItem : MonoBehaviour
{
    public Text Name;
    public Text Mmr;

    public void SetView(string name, string mmr)
    {
        Name.text = name;
        Mmr.text = mmr;
    }
}
