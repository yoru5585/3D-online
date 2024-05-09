using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    [SerializeField] GameObject ModeSelect;
    [SerializeField] GameObject RoomSetting;
    [SerializeField] GameObject RoomSearch;
    [SerializeField] GameObject SettingBG;
    [SerializeField] GameObject WaitingBG;

    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject ChatPanel;
    public void RoomSettingButton()
    {
        ModeSelect.SetActive(false);
        RoomSetting.SetActive(true);
        RoomSearch.SetActive(false);
    }

    public void RoomSearchButton()
    {
        ModeSelect.SetActive(false);
        RoomSetting.SetActive(false);
        RoomSearch.SetActive(true);
    }

    public void BackButton()
    {
        ModeSelect.SetActive(true);
        RoomSetting.SetActive(false);
        RoomSearch.SetActive(false);
        SettingBG.SetActive(true);
        WaitingBG.SetActive(false);
    }

    public void RoomJoinButton()
    {
        ModeSelect.SetActive(true);
        RoomSetting.SetActive(false);
        RoomSearch.SetActive(false);
        SettingBG.SetActive(false);
        WaitingBG.SetActive(true);
    }

    public void InfoButton()
    {
        ChatPanel.SetActive(false);
        if (InfoPanel.activeSelf)
        {
            InfoPanel.SetActive(false);
        }
        else
        {
            InfoPanel.SetActive(true);
        }
    }

    public void ChatButton()
    {
        InfoPanel.SetActive(false);
        if (ChatPanel.activeSelf)
        {
            ChatPanel.SetActive(false);
        }
        else
        {
            ChatPanel.SetActive(true);
        }
    }

    public void ExitButton()
    {
        ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData6", () =>
        {
            BackButton();
            GetComponent<GameLauncher>().LeftRoom();
        });
    }
}
