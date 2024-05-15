using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerAvatarController : MonoBehaviourPunCallbacks
{
    // プレイヤー名のネットワークプロパティを定義する

    [SerializeField]
    private PlayerAvatarView view;

	bool isStop = false;


    private void Start()
    {
        // プレイヤー名とプレイヤーIDを表示する
        view.SetNickName($"{photonView.Owner.NickName}({photonView.OwnerActorNr})");
    }

	[SerializeField]
	private Rigidbody rigidBody;
	//　移動速度
	private Vector3 velocity;
	//　入力値
	private Vector3 input;
	//　速さ
	[SerializeField]
	private float walkSpeed = 4f;
	void FixedUpdate()
	{
        if (isStop)
        {
			return;
        }

		if (photonView.IsMine)
		{
			//　入力値
			input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//　移動速度計算
			var clampedInput = Vector3.ClampMagnitude(input, 1f);
			velocity = clampedInput * walkSpeed;
			transform.LookAt(rigidBody.position + input);
			//　今入力から計算した速度から現在のRigidbodyの速度を引く
			velocity = velocity - rigidBody.velocity;
			//　速度のXZを-walkSpeedとwalkSpeed内に収めて再設定
			velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));
			//　移動処理
			rigidBody.AddForce(rigidBody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);


		}

	}

	public void SetStop(bool b)
    {
		isStop = b;
    }
}
