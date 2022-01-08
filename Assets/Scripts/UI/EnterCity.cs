using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnterCity : MonoBehaviour
{
    public GameScene gameScene;
    public GameObject CityOverlay;
    public Camera cam;

    public void Start()
    {
        gameScene = FindObjectOfType<GameScene>();
        CityOverlay = gameScene.CityOverlay;
        cam = Camera.main;
    }

    public void OnMouseDown()
    {
        /*CityOverlay.SetActive(true);
        gameScene.SelectCity(gameObject);*/
        Vector3 pos = transform.position;
        pos.y = cam.transform.position.y;
        pos.x -= 15;
        pos.z -= 15;
        cam.transform.position = pos;
    }
}
