using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {

    public static GameController controller;

	public Text pointsText;
	public Text coinsText;
	public Text highScoreText;
    public Text livesText;
    public Button pauseButton;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject redFlash;
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;

    public bool isPaused;
    public int score;

    private int highScore;
    private int coins;
    private float adCounter;
    private int currentShip;
    private int[] shipLevels;
    private float damageRate;
    private float fireRate;


	void Start(){
        controller = this;
        InitializeValuesFromData();
        adCounter += 1;
        Data.data.SetAdCounter(adCounter);
        if (adCounter % 5 == 0)
        {
            ShowAd();
        }
        isPaused = false;
        Time.timeScale = 1;
        LoadPlayer(currentShip);
        UpdateLivesText(3);
        UpdatePointsText();
    }

    //<========================================PRIVATE METHODS========================================>
    private void InitializeValuesFromData()
    {
        highScore = Data.data.GetHighScore();
        adCounter = Data.data.GetAdCounter();
        currentShip = Data.data.GetCurrentShip();
        shipLevels = Data.data.GetShipLevels();
        SetDamageRate();
        SetFireRate();
        coins = 0;
        score = 0;
    }

    private void SetDamageRate()
    {
        damageRate = ((1 + (2 * currentShip)) + (0.1f * shipLevels[currentShip]));
    }

    private void SetFireRate()
    {
        fireRate = ((8 + (2 * currentShip)) + (0.1f * shipLevels[currentShip]));
    }

    private void UpdateGameOverText()
    {
        highScoreText.text = "High Score: " + highScore;
        coinsText.text = "Coins: " + coins;
    }

    private void Flash()
    {
        ToggleOuch();
        Invoke("ToggleOuch", 0.1f);
    }

    private void UpdateLivesText(int health)
    {
        livesText.text = "Lives: " + health;
    }

    private void UpdatePointsText()
    {
        pointsText.text = "Points: " + score;
    }

    private void ToggleOuch()
    {
        if (redFlash.activeSelf)
            redFlash.SetActive(false);
        else
            redFlash.SetActive(true);
    }

    private void LoadPlayer(int ship)
    {
        switch (ship)
        {
            case 0:
                Instantiate(ship1, new Vector3(0, -5f, -2), Quaternion.identity);
                break;
            case 1:
                Instantiate(ship2, new Vector3(0, -5f, -2), Quaternion.identity);
                break;
            case 2:
                Instantiate(ship3, new Vector3(0, -5f, -2), Quaternion.identity);
                break;
        }
    }

    private void UpdateData()
    {
        coins += (int)(score / 4);
        if (score > highScore)
        {
            highScore = score;
            Data.data.SetHighScore(highScore);
        }
        Data.data.SetCoins(coins + Data.data.GetCoins());
        PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_asteroid_shooter_leaderboard, score);
        Data.data.Save();
    }

    private void ShowAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
    }

    //<========================================PUBLIC METHODS========================================>

    public float GetDamageRate()
    {
        return damageRate;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public void Reload()
    {
        ObjectPooler.objectPools.Clear();
        SceneManager.LoadScene("GameView");
    }

    public void LoadMenu()
    {
        ObjectPooler.objectPools.Clear();
        SceneManager.LoadScene("MenuView");
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdatePointsText();
    }

    public void PlayerWasHit(int health)
    {
        Flash();
        UpdateLivesText(health);
    }

    public void PlayerWasKilled()
    {
        Time.timeScale = 0;
        UpdateData();
        UpdateGameOverText();
        gameOverScreen.SetActive(true);
    }

    public void TogglePaused()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
    }

    public void ShowLeaderboard()
    {
        PlayGamesScript.ShowLeaderboard();
    }

}
