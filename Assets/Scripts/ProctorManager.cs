using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProctorManager : MonoBehaviour {
    public SteamVR_LoadLevel levelManager;

    private int numberOfCivilians = 0, numberOfEnemies = 0;
    private GameManager.IntelType typeOfIntel;
	private GameManager.HostilityLevel hostilityLevel;
	private string levelName = "MainScene";

    public Text intelLabel, hostilityLabel, civiliansLabel, enemiesLabel;
    public Slider civilianSlider, enemiesSlider;

	private GameManager GM;

	void Start () {
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        SetHostilityLevel();
        SetTypeOfIntel();
	}

    public int GetNumberOfCivilians() {
        return numberOfCivilians;
    }

    public int GetNumberOfEnemies() {
        return numberOfCivilians;
    }

	public GameManager.IntelType GetTypeOfIntel() {
        return typeOfIntel;
    }

	public GameManager.HostilityLevel GetHostilityLevel() {
        return hostilityLevel;
    }

    public void SetNumberOfCivilians(int num) {
        numberOfCivilians = num;
    }

    public void SetNumberOfCivilians() {
        numberOfCivilians = (int)civilianSlider.value;
        Debug.Log(numberOfCivilians);
		GM.numberOfCivilians = numberOfCivilians;
    }

    public void SetNumberOfEnemies(int num) {
        numberOfEnemies = num;
    }

    public void SetNumberOfEnemies() {
        numberOfEnemies = (int)enemiesSlider.value;
		GM.numberOfEnemies = numberOfEnemies;
        Debug.Log(numberOfEnemies);
    }

	public void SetTypeOfIntel(GameManager.IntelType type) {
        typeOfIntel = type;
    }

	public void SetHostilityLevel(GameManager.HostilityLevel level) {
        hostilityLevel = level;
    }

    public void SetHostilityLevel() {
        string level = hostilityLabel.text;

        switch (level) {
            case "Low Hostility":
			hostilityLevel = GameManager.HostilityLevel.Low;
                Debug.Log("Low Hostility");
                break;
            case "Medium Hostility":
                Debug.Log("Med Hostility");
			hostilityLevel = GameManager.HostilityLevel.Medium;
                break;
            case "High Hostility":
                Debug.Log("High Hostility");
			hostilityLevel = GameManager.HostilityLevel.High;
                break;
            default:
                Debug.LogError("The hostility level passed was not a valid string.");
                break;
        }
		GM.hostility = hostilityLevel;
    }

    public void SetTypeOfIntel() {
        string type = intelLabel.text;

        switch (type) {
            case "Bomb Material":
                Debug.Log("Bomb Material");
			typeOfIntel = GameManager.IntelType.BombMaterial;
                break;
            case "Hard Drive":
                Debug.Log("Hard Drive");
			typeOfIntel = GameManager.IntelType.HardDrive;
                break;
            case "Information":
                Debug.Log("Information");
			typeOfIntel = GameManager.IntelType.Info;
                break;
            default:
                Debug.LogError("The intel type passed was not a valid string.");
                break;
        }
		GM.intel = typeOfIntel;
    }

    public void Submit() {
        Debug.Log("Submit pressed");

        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.numberOfCivilians = (int) civilianSlider.value;
        GM.numberOfEnemies = (int)enemiesSlider.value;
        GM.hostility = hostilityLevel;
        GM.intel = typeOfIntel;
		GM.shotsFired = 0;
        levelManager.levelName = levelName;
        levelManager.Trigger();
		GM.shotsFired = 0;
    }

    public void CivilianSliderChanged() {
        civiliansLabel.text = civilianSlider.value.ToString();
    }

    public void EnemiesSliderChanged() {
        enemiesLabel.text = enemiesSlider.value.ToString();
    }

}
