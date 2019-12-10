using UnityEngine;
using System.Collections;
using TMPro;

public class HUD : MonoBehaviour
{
	public TextMeshProUGUI xp_text;
	public TextMeshProUGUI coin_text;
	public TextMeshProUGUI waves_text;

	private static HUD instance;
	public static HUD Instance
	{
		get
		{
			return instance;
		}
	}

	private void Start()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public void UpdateHud(int xp, int curWaves, int allWaves)
	{
		xp_text.text = xp.ToString();
		waves_text.text = curWaves + "/" + allWaves;
	}

	public void UpdateCoinsCount(int coins)
	{
		coin_text.text = coins.ToString();
	}
}
