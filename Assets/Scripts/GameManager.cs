using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum HostilityLevel { Low, Medium, High };
	public enum IntelType { BombMaterial, HardDrive, Info };

	public int numberOfEnemies;
	public int numberOfCivilians;
	public int shotsFired;
	public HostilityLevel hostility;
	public IntelType intel;

	public int numberOfEnemiesShot;
	public int numberOfCiviliansShot;
    
    public float timer;

	private const int enemyShotScore = 10;
	private const int enemyMissedScore = -50;
	private const int civilianShotScore = -25;
	private const int missedShotScore = -5;

	private float[] scoreMultiplier = {1.5f, 1.4f, 1.3f, 1.2f, 1.1f, 1.0f, 0.9f, 0.8f, 0.7f, 0.6f};

	private bool shouldTimerBeRunning;
    


	public static GameManager currentInstance;

	void Awake () {
		numberOfCiviliansShot = 0;
		numberOfEnemiesShot = 0;
		if(!currentInstance) {
			currentInstance = this;
			DontDestroyOnLoad(gameObject);
		} else 
			Destroy(gameObject);
	}

    private void OnLevelWasLoaded(int level) {
        timer = 0.0f;
		shouldTimerBeRunning = true;
    }

    void Update() {
		if (shouldTimerBeRunning) {
			timer += Time.deltaTime;
		}
		print(GetScore());
    }

	public void StopTimer() {
		shouldTimerBeRunning = false;
	}

    public string GetDate() {
        int minutes = (int) Mathf.Floor((timer/60.0f));
        float seconds = timer - (minutes * 60);
		return minutes.ToString("00") + ":" + seconds.ToString("00.00");
    }

	public string GetScore() {
		return CalculateScore().ToString();
	}

	private int CalculateScore() {
		int score = 0;
		score += numberOfEnemiesShot * enemyShotScore;
		score += numberOfCiviliansShot * civilianShotScore;
		score += (shotsFired - numberOfCiviliansShot - numberOfEnemiesShot) * missedShotScore;
        if (!shouldTimerBeRunning) {
            score += (numberOfEnemies - numberOfEnemiesShot) * enemyMissedScore;
            if (Mathf.Floor(timer / 60) <= scoreMultiplier.Length) {
                score = (int)Mathf.Floor(score * scoreMultiplier[(int)Mathf.Floor(timer / 60)]);
            }
            else {
                score = (int)(score * 0.5f);
            }
        }
		return score;
	}
}
