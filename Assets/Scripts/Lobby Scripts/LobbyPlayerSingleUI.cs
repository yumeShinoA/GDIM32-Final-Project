using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class LobbyPlayerSingleUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Button kickPlayerButton;
    [SerializeField] private Button startGameButton;


    private Player player;


    private void Awake()
    {
        kickPlayerButton.onClick.AddListener(KickPlayer);
        startGameButton.onClick.AddListener(StartGame);
    }

    public void SetKickPlayerButtonVisible(bool visible)
    {
        kickPlayerButton.gameObject.SetActive(visible);
    }

    public void setStartButtonVisible(bool visible)
    {
        startGameButton.gameObject.SetActive(visible);
    }

    public void UpdatePlayer(Player player)
    {
        this.player = player;
        playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;

    }

    private void KickPlayer()
    {
        if (player != null)
        {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }

    private void StartGame()
    {
        LobbyManager.Instance.StartGame();
    }

}