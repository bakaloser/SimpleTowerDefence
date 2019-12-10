using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower", order = 54)]
public class TowerData : ScriptableObject
{
	[SerializeField]
	private string towerName = "Tower";
	[SerializeField]
	private int price;
	[SerializeField]
	private int range;
	[SerializeField]
	private float shoot_interval;
	[SerializeField]
	private int damage;
	[SerializeField]
	private Sprite icon;
	[SerializeField]
	private Sprite weapon_icon;
	[SerializeField]
	private float weapon_speed;

	public string TowerName => towerName;
	public int Price => price;
	public int Range => range;
	public float ShootInterval => shoot_interval;
	public int Damage => damage;
	public Sprite Icon => icon;
	public Sprite WeaponIcon => weapon_icon;
	public float WeaponSpeed => weapon_speed;
}
