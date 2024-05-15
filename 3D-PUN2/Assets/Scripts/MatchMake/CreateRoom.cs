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
        //参加人数が正しい数値か確認
        bool result = int.TryParse(playerNumInputField.text, out num);
        if (!result || num < 2)
        {
            ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData8");
        }
    }

    public void CheckExistRoom(string input)
    {
        //既に同じ名前の部屋が存在しないか確認
        if (GetComponent<RoomListManager>().GetRoomList(input) != null)
        {
            ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData9");
        }
    }
}
