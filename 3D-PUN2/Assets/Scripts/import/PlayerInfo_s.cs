using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class PlayerInfo_s : ScriptableObject
{
    [Header("プレイヤー名")] public string playerName;
    [Header("プレイヤーID")] public int playerID;
}
