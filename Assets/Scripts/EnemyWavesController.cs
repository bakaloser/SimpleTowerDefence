using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
	public Enemy enemyPrefab;
	public List<EnemyWavesData> enemyWaves;

	[HideInInspector]
	public List<Enemy> enemies;

	private int CurWave => Main.Instance.levelController.CurrentWaveCount;
	private int AllWave => Main.Instance.levelController.LevelWavesCount;
	private float waveDuration;
	public enum WavesTypes
	{
		SimpleWave,
		MiddleWave,
		HardWave
	}

	private static EnemyWavesController instance;
	public static EnemyWavesController Instance
	{
		get
		{
			return instance;
		}
	}

	private void Start()
	{
		instance = this;
		enemies = new List<Enemy>();
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public void CreateEnemyWave(WavesTypes type)
	{
		if(enemies == null)
			enemies = new List<Enemy>();
		int curWave = Main.Instance.levelController.CurrentWaveCount;
		switch (type)
		{
			case WavesTypes.SimpleWave:
			default:
				if (curWave >= 0 && curWave < enemyWaves.Count)
					CreateEnemyWave(enemyWaves[curWave]);
				break;
		}
	}

	public void CreateEnemyWave(EnemyWavesData wave)
	{
		waveDuration = wave.WaveDuration;
		int count = wave.EnemysCount;

		for(int i = 0; i < count; i++)
		{
			Enemy enemy = Instantiate(enemyPrefab, transform, false);
			enemy.transform.localPosition = Main.Instance.firstPathPos + Vector3.left * 0.01f * enemyPrefab.GetEnemyWidth * i;
			enemy.Data = wave.EnemyType;
			enemies.Add(enemy);
		}

		Main.Instance.levelController.CurrentWaveCount++;
	}

	public void Update()
	{
		if (Main.isPause)
			return;

		waveDuration -= Time.deltaTime;
		if (waveDuration < 0 && CurWave < AllWave)
		{
			CreateEnemyWave(WavesTypes.SimpleWave);
		}

		if (CurWave == AllWave && enemies.Count == 0)
			Main.Instance.LevelFinish();
	}

	public void DestroyEnemy(Enemy en)
	{
		if (enemies.Contains(en))
		{
			enemies.Remove(en);
			Destroy(en.gameObject);
		}

		if (CurWave == AllWave && enemies.Count == 0)
			Main.Instance.LevelFinish();
	}
}
