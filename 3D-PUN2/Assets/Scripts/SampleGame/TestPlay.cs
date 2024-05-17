using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPlay : MonoBehaviour
{
    [SerializeField] bool IsTestPlaying = false;
    [SerializeField] GameObject debugLogCanvas;
    [SerializeField] TMP_Text indexText;
    GameObject myCamera;
    List<GameObject> dammyPlayers;
    ReplacePlayerAvatar replacePlayerAvatar;
    int index = 0; 
    private void Awake()
    {
        if (IsTestPlaying)
        {
            //参照
            replacePlayerAvatar = GetComponent<ReplacePlayerAvatar>();

            //オブジェクト代入
            myCamera = replacePlayerAvatar.GetCameraObj();
            dammyPlayers = replacePlayerAvatar.GetPlayerAvatarList();
            

            //enabled false
            replacePlayerAvatar.enabled = false;

            //カメラをダミーに移動
            myCamera.transform.parent = dammyPlayers[index].transform;
            myCamera.transform.localPosition = new Vector3(0, 2.4f, 0);

            //rigidbodyをセット
            GetComponent<TestPlayerController>().SetRigidbody(dammyPlayers[index].GetComponent<Rigidbody>());

            //現在操作中のプレイヤーの番号を表示
            indexText.text = index.ToString();

            //デバッグ用キャンパス表示
            debugLogCanvas.SetActive(true);

            //動けるようにする
            GetComponent<TestPlayerController>().SetIsStop(false);
        }
    }

    //ほかのプレイヤー視点に切り替える
    public void ChangeTargetDammyPlayer()
    {
        index++;
        if (index >= dammyPlayers.Count) index = 0;

        myCamera.transform.parent = dammyPlayers[index].transform;
        myCamera.transform.localPosition = new Vector3(0, 2.4f, 0);
        GetComponent<TestPlayerController>().SetRigidbody(dammyPlayers[index].GetComponent<Rigidbody>());

        //現在操作中のプレイヤーの番号を表示
        indexText.text = index.ToString();
    }
}
