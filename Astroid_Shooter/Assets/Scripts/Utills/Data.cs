using UnityEngine;

public class Data : MonoBehaviour {
    
	public static Data data;

    private int currentShip;
    private bool hasGameData;
    private int[] shipOwnership = new int[3];
    private int[] shipLevels = new int[3];
    private int[] shipLevelCosts = new int[3];
    private int coins;
    private int highScore;
    private float adCounter;

    public int GetCurrentShip() { return currentShip; }
    public void SetCurrentShip(int s) { currentShip = s; }

    public int[] GetShipOwnership() { return shipOwnership; }
    public void SetShipOwnership(int[] so) { shipOwnership = so; }

    public int[] GetShipLevels() { return shipLevels; }
    public void SetShipLevels(int[] sl) { shipLevels = sl; }

    public int[] GetShipLevelCosts() { return shipLevelCosts; }
    public void SetShipLevelCosts(int[] slc) { shipLevelCosts = slc; }

    public int GetCoins() { return coins; }
    public void SetCoins(int c) { coins = c; }

    public int GetHighScore() { return highScore; }
    public void SetHighScore(int hs) { highScore = hs; }

    public float GetAdCounter() { return adCounter; }
    public void SetAdCounter(float ac) { adCounter = ac; }

    void Awake(){
		if (data == null) {
			DontDestroyOnLoad (gameObject);
			data = this;
		}else if(data != this){
			Destroy (gameObject);
		}

        if (PlayerPrefs.HasKey("HasGameData"))
        {
            Load();
        }
        else
        {
            SetDefault();
            hasGameData = true;
            Save();
        }
    }

	public void SetDefault(){
        shipOwnership[0] = 0;
        shipOwnership[1] = 1;
        shipOwnership[2] = 1;
        shipLevels[0] = 1;
        shipLevels[1] = 1;
        shipLevels[2] = 1;
        shipLevelCosts[0] = 20;
        shipLevelCosts[1] = 20;
        shipLevelCosts[2] = 20;
        adCounter = 0;
        coins = 0;
		highScore = 0;
	}
    
	public void Save(){
		PlayerPrefs.SetInt ("HasGameData", (hasGameData ? 1 : 0));
		PlayerPrefs.SetInt ("Coins", coins);
		PlayerPrefs.SetInt ("HighScore", highScore);
        PlayerPrefs.SetFloat("AdCounter", adCounter);
        PlayerPrefsX.SetIntArray("ShipOwnership", shipOwnership);
        PlayerPrefsX.SetIntArray("ShipLevels", shipLevels);
        PlayerPrefsX.SetIntArray("ShipLevelCosts", shipLevelCosts);
    }

	public void Load(){
		hasGameData = (PlayerPrefs.GetInt ("SasGameData") != 0);
        adCounter = PlayerPrefs.GetFloat("AdCounter");
		coins = PlayerPrefs.GetInt ("Coins");
		highScore = PlayerPrefs.GetInt ("HighScore");
        shipOwnership = PlayerPrefsX.GetIntArray("ShipOwnership");
        shipLevels = PlayerPrefsX.GetIntArray("ShipLevels");
        shipLevelCosts = PlayerPrefsX.GetIntArray("ShipLevelCosts");
        if (shipLevelCosts.Length == 0)
        {
            shipLevelCosts = new int[3];
            shipLevelCosts[0] = 20;
            shipLevelCosts[1] = 20;
            shipLevelCosts[2] = 20;
        }
        if (shipLevels.Length == 0)
        {
            shipLevels = new int[3];
            shipLevels[0] = 1;
            shipLevels[1] = 1;
            shipLevels[2] = 1;
        }
    }
}
