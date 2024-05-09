using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    [SerializeField]
    private Camera myCamera;

    private void Start()
    {
        if (myCamera == null)
        {
            myCamera = Camera.main;
        }
    }
    // プレイヤー名をテキストに設定する
    public void SetNickName(string nickName)
    {
        nameLabel.text = nickName;
    }

    private void LateUpdate()
    {
        // プレイヤー名のテキストを、常にカメラの正面向きにする
        nameLabel.transform.rotation = myCamera.transform.rotation;
    }
}