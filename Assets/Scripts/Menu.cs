using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameObject createButtons;
	public GameObject sellButtons;

	[HideInInspector]
	public TowerSlotController curTowerSlot;
	[HideInInspector]
	public Tower curTower;

	public void CreateTower(string towerType)
	{
		curTowerSlot.CreateTower(towerType);
	}

	public void SellTower()
	{
		curTower.SellTower();
	}
}
