using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public void OnChangeInputText(TMP_InputField inputText)
    {
        if (inputText.text.Length > 6)
        {
            ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData7");
            return;
        }
        PlayerInfo_s playerinfo = Resources.Load<PlayerInfo_s>("PlayerInfo");
        playerinfo.playerName = inputText.text;
    }
}
