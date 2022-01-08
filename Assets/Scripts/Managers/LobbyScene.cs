using UnityEngine;
using MLAPI;
using MLAPI.SceneManagement;
using TMPro;

public class LobbyScene : MonoSingleton<LobbyScene>
{
    [SerializeField] private Animator animator;

    [SerializeField] public GameObject playerListItemPrefab;
    [SerializeField] public Transform playerListContainer;
    [SerializeField] public TMP_InputField playerNameInput;
    public TMP_Dropdown playerRace;
    public TMP_Dropdown playerCollor;


    #region buttons

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        animator.SetTrigger("Lobby");
    }
    public void Connect()
    {
        NetworkManager.Singleton.StartClient();
        animator.SetTrigger("Lobby");
    }

    public void Back()
    {
        animator.SetTrigger("Main");
        NetworkManager.Singleton.Shutdown();
    }

    public void StartGame()
    {
        NetworkSceneManager.SwitchScene("Game");
    }

    public void OnLobbySubmitNameChange()
    {
        string newName = playerNameInput.text;

        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedClient))
        {
            var player = networkedClient.PlayerObject.GetComponent<PlayerController>();
            if (player)
                player.ChangeName(newName);
        }
    }

    public void OnPlayerChangeRace()
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedClient))
        {
            var player = networkedClient.PlayerObject.GetComponent<PlayerController>();
            if (player)
            {
                int value = player.playerRaceDrop.value;
                player.ChangeRace(value);
            }
        }
    }

    public void OnPlayerChangeCollor()
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedClient))
        {
            var player = networkedClient.PlayerObject.GetComponent<PlayerController>();
            if (player)
            {
                int value = player.playerCollorDrop.value;
                player.ChangeCollor(value);
            }
        }
    }

    #endregion
}
