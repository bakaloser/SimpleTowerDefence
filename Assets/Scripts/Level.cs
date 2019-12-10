using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	public List<LevelData> data;
	public int LevelUserXp
	{
		get
		{
			return levelUserXp;
		}
		set
		{
			levelUserXp = value;
			HUD.Instance.UpdateHud(levelUserXp, curWaveCount, levelWavesCount);

			if (levelUserXp == 0)
				Main.Instance.GameOver();
		}
	}

	public int LevelWavesCount
	{
		get
		{
			return levelWavesCount;
		}
		set
		{
			levelWavesCount = value;
			HUD.Instance.UpdateHud(levelUserXp, curWaveCount, levelWavesCount);
		}
	}

	public int CurrentWaveCount
	{
		get
		{
			return curWaveCount;
		}
		set
		{
			curWaveCount = value;
			HUD.Instance.UpdateHud(levelUserXp, curWaveCount, levelWavesCount);
		}
	}

	private int levelUserXp;
	private int levelWavesCount;
	private int curWaveCount;

	public void CreateLevel(int number)
	{
		if (number < data.Count)
		{
			LevelUserXp = data[number].XP;
			LevelWavesCount = data[number].WavesCount;
		}
	}

	public void ReduceXp(int count)
	{
		LevelUserXp -= count;
	}

	public void IncreaseWave()
	{
		CurrentWaveCount++;
	}
}