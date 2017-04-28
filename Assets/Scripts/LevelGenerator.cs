using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject[] room1Configs;
	public GameObject[] room2Configs;
	public GameObject[] room3Configs;
	public GameObject[] room4Configs;

    public GameObject enemyTarget;
	public GameObject civilianTarget;

	public GameObject info;
	public GameObject bomb;
	public GameObject bmm;
	public GameObject harddrive;

	public AudioSource backgroundSound;

	private string gameManager = "GameManager";

    private IList<GameObject> rooms = new List<GameObject>();

	// Use this for initialization
	void Start () {
		GameObject GMGameObject = GameObject.Find (gameManager);

		if (GMGameObject != null) {
			GameManager GM = GMGameObject.GetComponent<GameManager>();
			Generate (GM.numberOfEnemies, GM.numberOfCivilians, GM.intel, GM.hostility);
		} else {
			Generate (5, 3, GameManager.IntelType.Info, GameManager.HostilityLevel.Medium);
		}
    }

	private void GenerateRoom(GameObject[] room) {
		int roomConfig = UnityEngine.Random.Range(0, 3);
        rooms.Add(GameObject.Instantiate(room[roomConfig]));
	}

	public void Generate(
        int enemies,
        int civilians,
		GameManager.IntelType intel,
		GameManager.HostilityLevel hostility
    ) {
        GenerateRoom(room1Configs);
        GenerateRoom(room2Configs);
        GenerateRoom(room3Configs);
        GenerateRoom(room4Configs);

		Spawn(enemies, "EnemySpawns", new GameObject[] {enemyTarget});
		Spawn(civilians, "CivilianSpawns", new GameObject[] {civilianTarget});

		if (intel == GameManager.IntelType.Info) {
			Spawn (4, "SmallIntelSpawns", new GameObject[] { info });
			Spawn (1, "LargeIntelSpawns", new GameObject[] { bmm });
			Spawn (1, "SmallIntelSpawns", new GameObject[] { bomb });
		} else if (intel == GameManager.IntelType.HardDrive){
			Spawn (4, "SmallIntelSpawns", new GameObject[] { harddrive });
			Spawn (1, "LargeIntelSpawns", new GameObject[] { bmm });
			//Spawn (1, "SmallIntelSpawns", new GameObject[] { bomb });
		} else {
			Spawn (3, "LargeIntelSpawns", new GameObject[] { bmm });
			Spawn (2, "SmallIntelSpawns", new GameObject[] { bomb });
		}

		switch (hostility) {

			case(GameManager.HostilityLevel.Low):
				backgroundSound.volume = 0.1f;
				break;
			case(GameManager.HostilityLevel.Medium):
				backgroundSound.volume = 0.5f;
				break;
			case(GameManager.HostilityLevel.High):
				backgroundSound.volume = 1;
				break;
			default:
				break;
		}
		backgroundSound.Play();
    }

    private int GetAvailableTargets(String holderName)
    {
        int targets = 0;
        foreach (GameObject room in rooms)
        {
            Transform spawns = room.transform.FindChild(holderName);
            targets += spawns.childCount;
        }
        return targets;
    }

    private void Spawn(int count, String holderName, GameObject[] gameObjects) {
        int availableTargets = GetAvailableTargets(holderName);

        while (count != 0 && availableTargets != 0)
        {
            foreach (GameObject room in rooms)
            {
                Transform spawns = room.transform.FindChild(holderName);

				while (count != 0 && availableTargets != 0)
                {
                    int i = UnityEngine.Random.Range(0, spawns.childCount);
					Transform spawn = spawns.GetChild(i);

					if (spawn.childCount == 0)
                    {
						GameObject child = GameObject.Instantiate(
							gameObjects[UnityEngine.Random.Range(0, gameObjects.Length)],
							spawn.position,
							spawn.rotation
                        );
						child.transform.parent = spawn;

						count--;
						availableTargets--;
						break;
					}
                }
            }
        }
    }
}
