using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerSlotController : MonoBehaviour
{
	public List<TowerData> tower_types;

	private Tower Tower_pref => Main.Instance.towerPrefab;
	private Menu GetMenu => Main.Instance.menu;

	private void OnMouseDown()
	{
		if (Main.isPause)
			return;

		GetMenu.gameObject.SetActive(true);
		GetMenu.transform.position = transform.position;
		GetMenu.curTowerSlot = this;
		GetMenu.createButtons.SetActive(true);
	}

	public void CreateTower(string towerType)
	{
		TowerData curData = tower_types.Find(x => x.name == towerType);

		if (curData != null)
		{
			if (Main.Instance.User.Coins >= curData.Price)
			{
				GetMenu.gameObject.SetActive(false);
				GetMenu.createButtons.SetActive(false);
				GetMenu.sellButtons.SetActive(false);

				Tower curTower = Instantiate(Tower_pref, Main.Instance.towersContainer, false);
				curTower.transform.position = this.transform.position;
				curTower.Data = curData;
				Main.Instance.User.CoinsReduce(curTower.Price);

				Destroy(gameObject);
			}
		}
		else
			Debug.LogError("Current tower type not found!");
	}
}