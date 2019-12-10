using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
	public Level levelController;
	public Transform groundContainer;
	public Transform towersContainer;
	public GameObject grassPrefab;
	public GameObject groundPrefab;
	public GameObject castleGroundPrefab;
	public GameObject castlePrefab;
	public GameObject towerSlotPrefab;
	public Tower towerPrefab;
	public Menu menu;
	public EnemyWavesController wavesController;
	public Message msg;

	private static Main instance;
	public static Main Instance
	{
		get
		{
			return instance;
		}
	}

	private int count_v;
	private int count_h;
	private int[,] map; // -1 - grass, 0 - ground , 5 - tower, 2 - castle_ground, 3 - castle

	private User user;
	public User User => user;

	public float GetCellWidth => grassPrefab.GetComponent<SpriteRenderer>().sprite.rect.width;
	public float GetCellHeight => grassPrefab.GetComponent<SpriteRenderer>().sprite.rect.height;
	
	private Vector3 StartVector => Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
	private float StartWorldPosX => -GetCellWidth / 2 + StartVector.x * 100f;
	private float StartWorldPosY => -GetCellHeight / 2 + StartVector.y * 100f;

	[HideInInspector]
	public Vector3 firstPathPos = new Vector3();
	[HideInInspector]
	public Vector3 lastPathPos = new Vector3();
	[HideInInspector]
	public Grid grid;
	public static bool isPause;

	private string mapFileName = "map";

	private void Start()
	{
		instance = this;
		user = new User();
		levelController.CreateLevel(0);
		GenerateMap();
		grid = new Grid();
		grid.CreateGrid(map);
		wavesController.CreateEnemyWave(EnemyWavesController.WavesTypes.SimpleWave);
;	}

	private void OnDestroy()
	{
		instance = null;
	}

	private void CreateMap()
	{
		TextAsset asset = Resources.Load<TextAsset>(mapFileName);
		List<string> rows = asset.text.Split('\n').ToList().FindAll(x => x.Length > 0);
		count_v = rows.Count;
		count_h = rows[0].Split(',').Length;
		map = new int[count_v, count_h];
		for(int i = 0; i < rows.Count; i++)
		{
			string[] columns = rows[i].Split(',');
			for(int j = 0; j < columns.Length; j++)
			{
				string cur = columns[j].Trim().Trim('\r');
				int val = -1;
				if (Int32.TryParse(cur, out val))
					map[i, j] = val;
				else
					map[i, j] = -1;
			}
		}
	}

	public Vector3 GetCellPosition(int i, int j)
	{
		return new Vector3((j * GetCellHeight + StartWorldPosX) * 0.01f, ((count_v - 1 - i) * GetCellWidth + StartWorldPosY) * 0.01f, 0);
	}

	private void GenerateMap()
	{
		CreateMap();

		firstPathPos = Vector3.zero;
		lastPathPos = Vector3.zero;

		for (int i = 0; i < count_v; i++)
		{
			for (int j = 0; j < count_h; j++)
			{
				GameObject prefab;
				Transform parent = groundContainer;
				Vector3 pos = GetCellPosition(i, j);
				if (map[i, j] == 0)
				{
					prefab = groundPrefab;
					if (j == 0 && firstPathPos == Vector3.zero)
						firstPathPos = pos;
					if (map[i, j + 1] == 3)
						lastPathPos = pos;
				}
				else if (map[i, j] == 5)
				{
					prefab = towerSlotPrefab;
					parent = towersContainer;
				}
				else if (map[i, j] == 2)
					prefab = castleGroundPrefab;
				else if (map[i, j] == 3)
					prefab = castlePrefab;
				else
					prefab = grassPrefab;

				GameObject new_cell = Instantiate(prefab, parent);
				new_cell.transform.position = pos;
			}
		}
	}

	private void ResetAll()
	{
		isPause = true;
		EnemyWavesController.Instance.enemies.ForEach(x => Destroy(x.gameObject));
	}

	public void GameOver()
	{
		ResetAll();
		msg.ShowGOMsg();
	}

	public void LevelFinish()
	{
		ResetAll();
		msg.ShowWinMsg();
	}

	public void RestartLevel()
	{
		levelController.CurrentWaveCount = 0;
		Tower[] towers = towersContainer.GetComponentsInChildren<Tower>();
		if(towers.Length > 0)
		{
			towers.ToList().ForEach(x => x.SellTower());
		}
		user.ResetAll();
		levelController.CreateLevel(0);
		isPause = false;

		wavesController.CreateEnemyWave(EnemyWavesController.WavesTypes.SimpleWave);
	}
}