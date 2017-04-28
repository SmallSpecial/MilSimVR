using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveProctorGUI : MonoBehaviour {
	public GameManager GM;
	public Text civsKilled, enemiesKilled, shotsFired, timer, score;

	public SteamVR_LoadLevel levelManager;
	private string levelName = "MainScene";

	// Use this for initialization
	void Start () {
		GameObject gameManager = GameObject.Find("GameManager");
		if (gameManager) {
			GM = gameManager.GetComponent<GameManager>();
		} else {
			GM = new GameManager();
			GM.numberOfCiviliansShot = 3;
			GM.numberOfEnemiesShot = 5;
			GM.shotsFired = 10;
		}
	}
	
	// Update is called once per frame
	void Update () {
		civsKilled.text = GM.numberOfCiviliansShot.ToString();
		enemiesKilled.text = GM.numberOfEnemiesShot.ToString();
		shotsFired.text = GM.shotsFired.ToString();
        if (GM != null) {
            timer.text = GM.GetDate();
        }
        if (GM != null) {
            score.text = GM.GetScore();
            //score.text = "100";
        }
    }

	public void Reset(){
		GM.numberOfCiviliansShot = 0;
		GM.numberOfEnemiesShot = 0;
		GM.shotsFired = 0;

		levelManager.levelName = levelName;
		levelManager.Trigger();
	}

    public void Stop() {
        GM.StopTimer();
    }
}
