using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform followTransform;
    public Transform cameraTransform;
    public Transform centerCamera;
    public GameObject player;
    public GameObject EndPos;

    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public float zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public float newZoom;

    public Camera cam;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    bool lerpStoped = false;

    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cam.orthographicSize;
        EndPos = GameObject.FindGameObjectWithTag("EndPos");
    }

    void Update()
    {
        if (followTransform != null && player != null)
        {
            //Debug.Log(lerpStoped);
            if (Math.Abs(transform.position.x - followTransform.position.x) > .5f && Math.Abs(transform.position.z - followTransform.position.z) > .5f && player.GetComponent<CameraFollowsPlayer>().playerClicked && !lerpStoped)
            {
                if (Math.Abs(transform.position.x - followTransform.position.x) > .5f && Math.Abs(transform.position.z - followTransform.position.z) > .5f && EndPos.GetComponent<MoveTarget>().isMoving)
                    Debug.Log("tacno");
                transform.position = Vector3.Lerp(transform.position, centerCamera.position, Time.deltaTime * 10f);
                if (Input.GetMouseButton(0))
                {
                    if (Math.Abs(transform.position.x - followTransform.position.x) < 0.5f && Math.Abs(transform.position.z - followTransform.position.z) < 0.5f)
                    {
                        transform.position = centerCamera.position;
                        lerpStoped = true;
                    }
                }
            }
            else
            {
                if (player.GetComponent<CameraFollowsPlayer>().playerClicked)
                    Debug.Log("True");
                if (player.GetComponent<CameraFollowsPlayer>().playerClicked)
                {
                    Debug.Log("OVDE");
                    transform.position = centerCamera.position;
                    newPosition = transform.position;
                    newRotation = transform.rotation;
                    player.GetComponent<CameraFollowsPlayer>().playerClicked = false;
                    lerpStoped = false;
                }
                HandleMovementInput();
                HandleMouseInput();
            }
        }
        else
        {
            HandleMovementInput();
            HandleMouseInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            newPosition = transform.position;
            newRotation = transform.rotation;
            followTransform = null;
            player.GetComponent<CameraFollowsPlayer>().playerChosen = false;
        }
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (cam.orthographicSize - Input.mouseScrollDelta.y >= 2f)
                cam.orthographicSize -= Input.mouseScrollDelta.y;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (plane.Raycast(ray, out float entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (plane.Raycast(ray, out float entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            cam.orthographicSize -= .1f;
        }
        if (Input.GetKey(KeyCode.F))
        {
            cam.orthographicSize += .1f;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
}
