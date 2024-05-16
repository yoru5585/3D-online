using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField] Button Button;
    // Start is called before the first frame update
    void Start()
    {
        GameObject networkManager = GameObject.FindGameObjectWithTag("NetworkController");
        Button.onClick.AddListener( () => networkManager.GetComponent<NetworkDisconnect>().RpcEndGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
