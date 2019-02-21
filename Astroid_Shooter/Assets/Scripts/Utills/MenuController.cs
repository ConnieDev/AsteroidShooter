using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Button ship1Button;
    public Button ship2Button;
    public Button ship3Button;
    public Button ship1LevelButton;
    public Button ship2LevelButton;
    public Button ship3LevelButton;
    public Button ship2StoreButton;
    public Button ship3StoreButton;

    public Text coinsText;
    public Text highScoreText;
    public Text ship1LevelText;
    public Text ship2LevelText;
    public Text ship3LevelText;
    public Text ship1LevelCostText;
    public Text ship2LevelCostText;
    public Text ship3LevelCostText;
    public Text ship2CostText;
    public Text ship3CostText;

    public Sprite locked;
    public Sprite ship02Sprite;
    public Sprite ship03Sprite;

    private int[] shipOwnership;
    public int[] shipLevels;
    public int[] shipLevelCosts;
    public int coins;
    private int highScore;
    private int shipnum;

    private void Start()
    {
        InitializeValuesFromData();
        ActivateButtons();
        ActivateStoreButtons();
        ActivateLevelUpButtons();
        UpdateLevelText();
        UpdateText();
    }

    //<========================================PRIVATE METHODS========================================>

    private void InitializeValuesFromData()
    {
        shipOwnership = Data.data.GetShipOwnership();
        shipLevels = Data.data.GetShipLevels();
        shipLevelCosts = Data.data.GetShipLevelCosts();
        highScore = Data.data.GetHighScore();
        coins = Data.data.GetCoins();
    }

    private void UpdateText()
    {
        coinsText.text = "Coins: " + coins;
        highScoreText.text = "High Score: " + highScore;
    }

    private void ActivateButtons()
    {
        ship1Button.enabled = true;
        ship2Button.enabled = (shipOwnership[1] == 0);
        ship3Button.enabled = (shipOwnership[2] == 0);

        if (shipOwnership[1] != 0)
            ship2Button.GetComponent<Image>().sprite = locked;
        else
            ship2Button.GetComponent<Image>().sprite = ship02Sprite;
        if (shipOwnership[2] != 0)
            ship3Button.GetComponent<Image>().sprite = locked;
        else
            ship3Button.GetComponent<Image>().sprite = ship03Sprite;
    }

    private void ActivateLevelUpButtons()
    {
        ship1LevelButton.enabled = true;
        ship2LevelButton.enabled = (shipOwnership[1] == 0);
        ship3LevelButton.enabled = (shipOwnership[2] == 0);

        if (shipOwnership[1] != 0)
            ship2LevelButton.GetComponent<Image>().sprite = locked;
        else
            ship2LevelButton.GetComponent<Image>().sprite = ship02Sprite;
        if (shipOwnership[2] != 0)
            ship3LevelButton.GetComponent<Image>().sprite = locked;
        else
            ship3LevelButton.GetComponent<Image>().sprite = ship03Sprite;
    }

    private void ActivateStoreButtons()
    {
        ship2StoreButton.enabled = (shipOwnership[1] == 1);
        ship3StoreButton.enabled = (shipOwnership[2] == 1);
        if (shipOwnership[1] == 0)
        {
            ship2CostText.text = "BOUGHT";
        }
        if (shipOwnership[2] == 0)
        {
            ship3CostText.text = "BOUGHT";
        }

    }

    private void SetShipnum(int num)
    {
        shipnum = num;
    }

    private void UpdateLevelText()
    {
        ship1LevelText.text = shipLevels[0].ToString();
        ship2LevelText.text = shipLevels[1].ToString();
        ship3LevelText.text = shipLevels[2].ToString();
        if (shipLevels[0] >= 100)
        {
            ship1LevelCostText.text = "MAX";
        }
        else
        {
            ship1LevelCostText.text = shipLevelCosts[0].ToString();
        }
        if (shipLevels[1] >= 100)
        {
            ship2LevelCostText.text = "MAX";
        }
        else
        {
            ship2LevelCostText.text = shipLevelCosts[1].ToString();
        }
        if (shipLevels[2] >= 100)
        {
            ship3LevelCostText.text = "MAX";
        }
        else
        {
            ship3LevelCostText.text = shipLevelCosts[2].ToString();
        }
    }

    //<========================================PUBLIC METHODS========================================>

    public void ShowLeaderboard()
    {
        PlayGamesScript.ShowLeaderboard();
    }

    public void ToggleScreen(GameObject screen)
    {
        if (screen.activeSelf)
            screen.SetActive(false);
        else
            screen.SetActive(true);
    }

    public void ChooseShip(int ship)
    {
        Data.data.SetCurrentShip(ship);
        SceneManager.LoadScene("GameView");
    }

    public void BuyShip(int price)
    {
        if (price <= coins) {
            shipOwnership[shipnum] = 0;
            coins -= price;
            Data.data.SetCoins(coins);
            ActivateButtons();
            ActivateStoreButtons();
            ActivateLevelUpButtons();
            UpdateText();
            Data.data.Save();
        }
    }

    public void LevelUp(int ship)
    {
        if (shipLevelCosts[ship] <= coins && shipLevels[ship] < 100)
        {
            coins -= shipLevelCosts[ship];
            Data.data.SetCoins(coins);
            shipLevels[ship] += 1;
            shipLevelCosts[ship] += 10;
            Data.data.SetShipLevels(shipLevels);
            Data.data.SetShipLevelCosts(shipLevelCosts);
            UpdateLevelText();
            UpdateText();
            Data.data.Save();
        }
    }
}
