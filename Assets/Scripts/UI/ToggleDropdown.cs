using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using TMPro;

public class ToggleDropdown : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(NetworkManager.Singleton.LocalClientId);
        Debug.Log(GetInstanceID());
        Debug.Log(NetworkObjectId);
        if (!IsServer && !NetworkManager.Singleton.LocalClientId.Equals(NetworkObjectId))
        {
            gameObject.GetComponent<TMP_Dropdown>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
