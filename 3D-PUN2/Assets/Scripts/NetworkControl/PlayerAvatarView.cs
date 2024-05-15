using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    private void Start()
    {
        
    }
    // プレイヤー名をテキストに設定する
    public void SetNickName(string nickName)
    {
        nameLabel.text = nickName;
    }
}