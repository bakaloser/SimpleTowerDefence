using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 53)]
public class EnemyData : ScriptableObject
{
	[SerializeField]
	private int health_amount;
	[SerializeField]
	private float moving_speed;
	[SerializeField]
	private int damage;
	[SerializeField]
	private Sprite icon;
	[SerializeField]
	private Vector2Int coinsRewardRange;

	public int Health => health_amount;
	public float Speed => moving_speed;
	public int Damage => damage;
	public Sprite Icon => icon;
	public Vector2Int RewardRange => coinsRewardRange;
}
