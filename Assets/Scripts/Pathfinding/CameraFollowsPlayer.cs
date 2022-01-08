using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : NetworkBehaviour
{
    public bool playerChosen = false;
    public bool playerClicked = false;

    public Material playerNotSelectedMaterial;
    public Material playerSelectedMaterial;
    public GameObject centerCamera;
    public GameObject EndPosition;

    public int OwnerID;

    private void Awake()
    {
        centerCamera = GameObject.FindGameObjectWithTag("CenterCamera");
        EndPosition = GameObject.FindGameObjectWithTag("EndPos");
    }

    private void Update()
    {
        if (playerChosen)
        {
            GetComponent<Renderer>().material = playerSelectedMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = playerNotSelectedMaterial;
        }
    }

    private void OnMouseDown()
    {
        if (!IsOwner)
            return;
        if (EndPosition.GetComponent<MoveTarget>().isMoving)
            return;
        foreach(GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            pl.GetComponent<CameraFollowsPlayer>().playerChosen = false;
            pl.GetComponent<CameraFollowsPlayer>().playerClicked = false;
            pl.GetComponent<MoveTo>().enabled = false;
        }
        foreach (GameObject dot in GameObject.FindGameObjectsWithTag("dot"))
        {
            Destroy(dot);
        }
        CameraController.instance.followTransform = transform;
        playerChosen = true;
        playerClicked = true;
        centerCamera.transform.position = transform.position;
        EndPosition.GetComponent<MoveTarget>().player = gameObject;
        EndPosition.GetComponent<MoveTarget>().CameraRig.GetComponent<CameraController>().player = gameObject;
        GetComponent<MoveTo>().enabled = true;
    }
}
