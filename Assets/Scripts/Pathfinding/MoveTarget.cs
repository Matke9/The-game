using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask hitLayers;
    public GameObject startPosition;
    public Transform StartPosition;
    public Transform TargetPosition;
    public GameObject player;
    public GameObject CameraRig;
    public bool isMoving = false;
    public List<GameObject> dots;
    static public bool wallClicked = false;

    private void Awake()
    {
        StartPosition = GameObject.FindGameObjectWithTag("StartPos").transform;
        startPosition = GameObject.FindGameObjectWithTag("StartPos");
        TargetPosition = GameObject.FindGameObjectWithTag("EndPos").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isMoving && player!=null)//If the player has left clicked
        {
            if (player.GetComponent<CameraFollowsPlayer>().playerChosen)
            {
                wallClicked = false;
                //Vector3 mouse = Input.mousePosition;//Get the mouse Position
                //Ray castPoint = Camera.main.ScreenPointToRay(mouse);//Cast a ray to get where the mouse is pointing at
                RaycastHit hit;//Stores the position where the ray hit.
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f);
                if (hit.transform.tag != "Wall" && hit.transform.tag != "Player")//If the raycast doesnt hit a wall
                {
                    Debug.Log("WE DID NOT HIT A WALL");
                    this.transform.position = hit.point;//Move the target to the mouse position
                    int i = player.GetComponent<MoveTo>().i;
                    dots = player.GetComponent<MoveTo>().dots;
                    var tuple = Pathfinding.FindPath(StartPosition.position, TargetPosition.position);
                    List<Node> FinalPath = Pathfinding.GetFinalPath(tuple.Item1, tuple.Item2);
                    foreach (var item in dots)
                    {
                        GameObject.Destroy(item);
                    }
                    isMoving = true;
                    startPosition.transform.position = player.transform.position;
                }
            }
        }
    }
}

