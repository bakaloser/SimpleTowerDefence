using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public SpriteRenderer icon;
	public Weapon weaponPref;
	[HideInInspector]
	public List<Node> rangeNodes;
	[HideInInspector]
	public Enemy currentTarget;

	public int Price => data.Price;
	private Menu GetMenu => Main.Instance.menu;
	private GameObject TowerSlotPref => Main.Instance.towerSlotPrefab;

	private TowerData data;
	private float shootInterval;
	private int damage;
	public TowerData Data
	{
		set
		{
			data = value;
			icon.sprite = data.Icon;
			damage = data.Damage;
			rangeNodes = Main.Instance.grid.GetAllNeighboringNodesByPos(transform.position, data.Range).FindAll(x => !x.isObstacle);
		}
	}

	private void OnMouseDown()
	{
		if (Main.isPause)
			return;

		GetMenu.gameObject.SetActive(true);
		GetMenu.transform.position = transform.position;
		GetMenu.curTower = this;
		GetMenu.createButtons.SetActive(false);
		GetMenu.sellButtons.SetActive(true);
	}

	public void SellTower()
	{
		GetMenu.gameObject.SetActive(false);
		GetMenu.createButtons.SetActive(false);
		GetMenu.sellButtons.SetActive(false);

		GameObject towerSlot = Instantiate(TowerSlotPref, Main.Instance.towersContainer, false);
		towerSlot.transform.position = this.transform.position;

		Main.Instance.User.CoinsIncrease(Price);

		Destroy(gameObject);
	}

	private void Update()
	{
		List<Enemy> enemiesInRadius = EnemyWavesController.Instance.enemies.FindAll(x => rangeNodes.Contains(x.CurNode));
		if (enemiesInRadius.Count > 0 && (currentTarget == null || !enemiesInRadius.Contains(currentTarget)))
		{
			Enemy nearEnemy = enemiesInRadius[0];
			foreach (Enemy en in enemiesInRadius)
			{
				if (Vector3.Distance(en.CurNode.vPosition, Main.Instance.lastPathPos) < Vector3.Distance(nearEnemy.CurNode.vPosition, Main.Instance.lastPathPos))
					nearEnemy = en;
			}
			currentTarget = nearEnemy;
		}
		else if (enemiesInRadius.Count == 0)
			currentTarget = null;

		if (currentTarget != null)
		{
			if (shootInterval > 0)
				shootInterval -= Time.deltaTime;
			else
				CreateWeapon();
		}
	}

	private void CreateWeapon()
	{
		shootInterval = data.ShootInterval;
		Weapon weapon = Instantiate(weaponPref, transform);
		weapon.CreateWeapon(data.WeaponIcon, damage, data.WeaponSpeed, currentTarget, this);
	}
}
