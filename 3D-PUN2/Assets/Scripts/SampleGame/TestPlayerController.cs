using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : BasicPlayerController
{
    private void Start()
    {
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStop) return;

        Move();
        CameraControll();
    }
    public void SetRigidbody(Rigidbody rd)
    {
        player_rg = rd;
    }

    public void SetIsStop(bool b)
    {
        IsStop = b;
    }
}
