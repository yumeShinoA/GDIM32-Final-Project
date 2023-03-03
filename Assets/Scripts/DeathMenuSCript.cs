using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuSCript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += OnDeathMenu;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= OnDeathMenu;
    }
    public void OnDeathMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
