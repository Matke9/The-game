using MLAPI.Messaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform StartPosition;
    public Transform TargetPosition;
    public Material sibo;
    private Vector3 velocity = Vector3.zero;
    public int i = 0;
    public GameObject dot;
    public List<GameObject> dots = new List<GameObject>();
    int brojac = 0;
    public int MovementLeft = 20;
    public int MovementLeftHelper = 20;

    [ServerRpc]
    public void MoveServer(Node n, float step) 
    {
        MoveClient(n,step);
    }

    [ClientRpc]
    public void MoveClient(Node n, float step)
    {
        transform.position = Vector3.MoveTowards(transform.position, n.vPosition, step);
    }

    private void Awake()//When the program starts
    {
        StartPosition = GameObject.FindGameObjectWithTag("StartPos").transform;
        TargetPosition = GameObject.FindGameObjectWithTag("EndPos").transform;
    }
    void Update()
    {
        var tuple = Pathfinding.FindPath(StartPosition.position, TargetPosition.position);
        List<Node> FinalPath = Pathfinding.GetFinalPath(tuple.Item1, tuple.Item2);
        if (FinalPath.Count > 0 && TargetPosition.GetComponent<MoveTarget>().isMoving)
        {
            if (brojac == 0)
                brojac++;
            //Debug.Log(FinalPath[0].vPosition);
            //transform.position = Vector3.SmoothDamp(transform.position, FinalPath[0].vPosition, ref velocity, 0.3f);
            if (MovementLeft > 0)
            {
                float step = speed * Time.deltaTime; // calculate distance to move
                //transform.position = Vector3.MoveTowards(transform.position, FinalPath[i].vPosition, step);
                MoveServer(FinalPath[i], step);
            }
            else {
                if (i + 1 < FinalPath.Count)
                {
                    i++;
                }
                else
                {
                    TargetPosition.GetComponent<MoveTarget>().isMoving = false;
                    i = 0;
                    brojac = 0;
                }
            }
            if (brojac == 1)
            {
                MovementLeftHelper = MovementLeft;
                for (int k = i; k < FinalPath.Count; k++)
                {
                    GameObject d;
                    dots.Add(d = GameObject.Instantiate(dot, FinalPath[k].vPosition, new Quaternion(0, 0, 0, 0)));
                    if (MovementLeftHelper <= 0)
                    {
                        d.GetComponent<Renderer>().material = sibo;
                    }
                    MovementLeftHelper--;
                }
                brojac = -1;
            }
            if (Vector3.Distance(transform.position, FinalPath[i].vPosition) < 0.001f)
            {
                if (i + 1 < FinalPath.Count)
                {
                    i++;
                }
                else
                {
                    TargetPosition.gameObject.GetComponent<MoveTarget>().isMoving = false;
                    i = 0;
                    brojac = 0;
                }
            }
        }
    }
}
