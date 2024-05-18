using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class TestPlay : MonoBehaviour
{
    [SerializeField] bool IsTestPlaying = false;
    [SerializeField] GameObject debugLogCanvas;
    [SerializeField] TMP_Text indexText;
    [SerializeField] GameObject dammyNetworkController;
    GameObject myCamera;
    List<GameObject> dammyPlayers;
    ReplacePlayerAvatar replacePlayerAvatar;
    int index = 0; 
    private void Awake()
    {
        if (IsTestPlaying)
        {
            PhotonNetwork.OfflineMode = true;
            //ダミーのネットワークコントローラを生成
            Instantiate(dammyNetworkController);

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

            //現在操作中のプレイヤーの番号を表示
            indexText.text = index.ToString();

            //デバッグ用キャンパス表示
            debugLogCanvas.SetActive(true);

            //1P以外動けないようにする
            for (int i = 1; i < dammyPlayers.Count; i++)
            {
                dammyPlayers[i].GetComponent<TestPlayerController>().SetIsStop(true);
            }
        }
    }

    //ほかのプレイヤー視点に切り替える
    public void ChangeTargetDammyPlayer()
    {
        index++;
        if (index >= dammyPlayers.Count) index = 0;

        myCamera.transform.parent = dammyPlayers[index].transform;
        myCamera.transform.localPosition = new Vector3(0, 2.4f, 0);
        for (int i = 0; i < dammyPlayers.Count; i++)
        {
            if (i == index)
            {
                dammyPlayers[index].GetComponent<TestPlayerController>().SetIsStop(false);
            }
            else
            {
                dammyPlayers[i].GetComponent<TestPlayerController>().SetIsStop(true);
            }
            
        }
        
        //現在操作中のプレイヤーの番号を表示
        indexText.text = index.ToString();
    }
}
