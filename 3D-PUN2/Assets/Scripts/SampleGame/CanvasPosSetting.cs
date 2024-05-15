using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPosSetting : MonoBehaviour
{
    List<Canvas> Canvases = new List<Canvas>();
    [SerializeField] Camera Camera;
    [SerializeField] int distance = 10;
    // Start is called before the first frame update
    void Start()
    {
        AddCanvasList();
        ChangeRenderMode();
    }

    void ChangeRenderMode()
    {
        foreach (Canvas canvas in Canvases)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera;
            canvas.planeDistance = distance;
        }
        
    }

    void AddCanvasList()
    {
        Canvas chatCanvas = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<Canvas>();
        Canvas RoomInfoCanvas = GameObject.FindGameObjectWithTag("RoomInfoPanel").GetComponent<Canvas>();
        Canvas ConWinCanvas = GameObject.FindGameObjectWithTag("ConWin").GetComponent<Canvas>();
        Canvases.Add(chatCanvas);
        Canvases.Add(RoomInfoCanvas);
        Canvases.Add(ConWinCanvas);
    }
}
