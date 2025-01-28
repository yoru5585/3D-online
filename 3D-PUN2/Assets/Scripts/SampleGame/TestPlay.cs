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
            //�_�~�[�̃l�b�g���[�N�R���g���[���𐶐�
            Instantiate(dammyNetworkController);

            //�Q��
            replacePlayerAvatar = GetComponent<ReplacePlayerAvatar>();

            //�I�u�W�F�N�g���
            myCamera = replacePlayerAvatar.GetCameraObj();
            dammyPlayers = replacePlayerAvatar.GetPlayerAvatarList();
            

            //enabled false
            replacePlayerAvatar.enabled = false;

            //�J�������_�~�[�Ɉړ�
            myCamera.transform.parent = dammyPlayers[index].transform;
            myCamera.transform.localPosition = new Vector3(0, 2.4f, 0);

            //���ݑ��쒆�̃v���C���[�̔ԍ���\��
            indexText.text = index.ToString();

            //�f�o�b�O�p�L�����p�X�\��
            debugLogCanvas.SetActive(true);

            //1P�ȊO�����Ȃ��悤�ɂ���
            for (int i = 1; i < dammyPlayers.Count; i++)
            {
                dammyPlayers[i].GetComponent<TestPlayerController>().SetIsStop(true);
            }
        }
    }

    //�ق��̃v���C���[���_�ɐ؂�ւ���
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
        
        //���ݑ��쒆�̃v���C���[�̔ԍ���\��
        indexText.text = index.ToString();
    }
}
