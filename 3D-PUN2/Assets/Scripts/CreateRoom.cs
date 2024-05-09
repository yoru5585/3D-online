using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] TMP_InputField roomInputField;
    [SerializeField] TMP_InputField playerNumInputField;
    [SerializeField] GameLauncher gameLauncher;
    int num = 2;
    public void OnRoomCreated()
    {
        gameLauncher.CreateRoom(roomInputField.text, num);
    }

    public void CheckFormat(string textIn)
    {
        bool result = int.TryParse(playerNumInputField.text, out num);
        if (!result)
        {
            ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData8");
        }
    }
}
