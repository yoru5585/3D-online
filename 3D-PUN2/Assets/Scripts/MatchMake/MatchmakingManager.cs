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

    GameObject RoomInfoPanel;
    GameObject ChatPanel;

    private void Start()
    {
        RoomInfoPanel = GameObject.FindGameObjectWithTag("RoomInfoPanel").transform.GetChild(0).gameObject;
        ChatPanel = GameObject.FindGameObjectWithTag("ChatPanel").transform.GetChild(0).gameObject;
    }

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
        if (RoomInfoPanel.activeSelf)
        {
            RoomInfoPanel.SetActive(false);
        }
        else
        {
            RoomInfoPanel.SetActive(true);
        }
    }

    public void ChatButton()
    {
        RoomInfoPanel.SetActive(false);
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
