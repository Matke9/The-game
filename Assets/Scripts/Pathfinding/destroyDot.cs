using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyDot : MonoBehaviour
{
    double time = 0;
    bool destroy = false;
    Collision clsn = null;
    void FixedUpdate()
    {
        /*
        Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        if (Vector3.Distance(player.transform.position, transform.position) < 0.5f)
        {
            // TODO
            Debug.Log("da");
            Destroy(gameObject);
        }
        */
        if (destroy)
        {
            time += Time.deltaTime;
            if (time >= 0.2)
            {
                clsn.gameObject.GetComponent<MoveTo>().MovementLeft--;
                Destroy(gameObject);
                time = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            clsn = collision;
            destroy = true;
        }
    }
}
