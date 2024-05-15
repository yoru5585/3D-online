using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ReplacePlayerAvatar : MonoBehaviourPunCallbacks
{
    //リストの順にプレイヤー１、プレイヤー２・・・と置き換わります。
    [SerializeField] List<GameObject> PlayerAvatarList;
    // Start is called before the first frame update
    void ReplaceMyAvater()
    {
        
        //PlayerAvatarList[Resources.Load<PlayerInfo_s>("PlayerInfo").playerID].transform.position;
    }

    public void ChangeAvatarParent()
    {

    }
}
