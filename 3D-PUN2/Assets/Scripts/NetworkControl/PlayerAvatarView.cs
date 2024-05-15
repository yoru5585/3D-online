using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    private void Start()
    {
        // プレイヤー名とプレイヤーIDを表示する
        SetNickName($"{photonView.Owner.NickName}({photonView.OwnerActorNr})");
    }
    // プレイヤー名をテキストに設定する
    public void SetNickName(string nickName)
    {
        nameLabel.text = nickName;
    }
}