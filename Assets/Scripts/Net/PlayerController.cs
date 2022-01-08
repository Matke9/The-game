using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    #region login screen network fields
    // Networked fields
    public NetworkVariableString playerName = new NetworkVariableString(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public NetworkVariableInt playerRace = new NetworkVariableInt(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public NetworkVariableInt playerCollor = new NetworkVariableInt(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    #endregion
    // Fields
    private GameObject myPlayerListItem;
    private TextMeshProUGUI playerNameLabel;
    public TMP_Dropdown playerRaceDrop;
    public TMP_Dropdown playerCollorDrop;
    public LobbyScene lobbyScene;
    public GameScene gameScene;
    public GameObject MovingPlayer;

    public override void NetworkStart()
    {
        RegisterEvents();

        myPlayerListItem = Instantiate(LobbyScene.Instance.playerListItemPrefab, Vector3.zero, Quaternion.identity);
        myPlayerListItem.transform.SetParent(LobbyScene.Instance.playerListContainer, false);

        playerNameLabel = myPlayerListItem.GetComponentInChildren<TextMeshProUGUI>();
        playerRaceDrop = myPlayerListItem.GetComponentsInChildren<TMP_Dropdown>()[0];
        playerCollorDrop = myPlayerListItem.GetComponentsInChildren<TMP_Dropdown>()[1];

        if (IsOwner)
        {
            playerName.Value = UnityEngine.Random.Range(1000, 9999).ToString();
        }
        else
        {
            playerNameLabel.text = playerName.Value;
            playerRaceDrop.value = playerRace.Value;
            playerCollorDrop.value = playerCollor.Value;
            playerCollorDrop.interactable = false;
            playerRaceDrop.interactable = false;
        }

        lobbyScene = FindObjectOfType<LobbyScene>();

        playerRaceDrop.onValueChanged.AddListener(delegate
        {
            lobbyScene.OnPlayerChangeRace();
        });

        playerCollorDrop.onValueChanged.AddListener(delegate
        {
            lobbyScene.OnPlayerChangeCollor();
        });
    }
    public void OnDestroy()
    {
        Destroy(myPlayerListItem);
        UnregisterEvents();
    }

    public void ChangeName(string newName)
    {
        if (IsOwner)
            playerName.Value = newName;
    }

    public void ChangeRace(int value)
    {
        if (IsOwner)
        {
            playerRace.Value = value;
        }
    }
    public void ChangeCollor(int value)
    {
        if (IsOwner)
        {
            playerCollor.Value = value;
        }
    }

    // Events
    private void RegisterEvents()
    {
        playerName.OnValueChanged += OnPlayerNameChange;
        playerRace.OnValueChanged += OnPlayerRaceChange;
        playerCollor.OnValueChanged += OnPlayerCollorChange;
    }
    private void UnregisterEvents()
    {
        playerName.OnValueChanged -= OnPlayerNameChange;
        playerRace.OnValueChanged -= OnPlayerRaceChange;
        playerCollor.OnValueChanged -= OnPlayerCollorChange;
    }

    private void OnPlayerNameChange(string previousValue, string newValue)
    {
        playerNameLabel.text = playerName.Value;
    }
    private void OnPlayerRaceChange(int previousValue, int newValue)
    {
        playerRaceDrop.value = playerRace.Value;
    }
    private void OnPlayerCollorChange(int previousValue, int newValue)
    {
        playerCollorDrop.value = playerCollor.Value;
    }
}
