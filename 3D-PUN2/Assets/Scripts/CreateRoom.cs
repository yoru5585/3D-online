using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] TMP_InputField roomInputField;
    [SerializeField] GameLauncher gameLauncher;
    public void OnRoomCreated()
    {
        gameLauncher.CreateRoom(roomInputField.text);
    }
}
