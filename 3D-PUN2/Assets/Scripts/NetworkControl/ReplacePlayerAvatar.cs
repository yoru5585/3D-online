using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UIElements;

public class ReplacePlayerAvatar : MonoBehaviour
{
    //リストの順にプレイヤー１、プレイヤー２・・・と置き換わります。
    [SerializeField] List<GameObject> PlayerAvatarList;

    [SerializeField] GameObject Camera;
    // Start is called before the first frame update

    private void Start()
    {
        ReplaceMyAvater();
    }

    //プレイヤーのネットワークオブジェクトを生成しダミーアバターと置き換える
    void ReplaceMyAvater()
    {
        GameObject myAvatar = PhotonNetwork.Instantiate("PlayerAvatar", Vector3.zero, Quaternion.identity);
        myAvatar.transform.position = PlayerAvatarList[Resources.Load<PlayerInfo_s>("PlayerInfo").playerID].transform.position;
        Camera.transform.parent = myAvatar.transform;
        Camera.transform.position = new Vector3(0, 2.4f, 0);
        foreach (GameObject obj in PlayerAvatarList)
        {
            Destroy(obj);
        }
    }


}
