using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public SpriteRenderer icon;
	public float GetEnemyWidth => icon.sprite.rect.width;
	public EnemyData Data
	{
		set
		{
			data = value;
			icon.sprite = data.Icon;
			speed = data.Speed;
			health = data.Health;
			damage = data.Damage;
		}
	}

	public Node CurNode => curNode;

	private EnemyData data;
	private float speed;
	private int health;
	private int damage;
	private List<Node> path;
	private Node curNode;
	private int nodeIndex = 0;

	private void Start()
	{
		Pathfind pathfind = new Pathfind();
		path = pathfind.FindPath();
		if (path != null && path.Count > 0)
			curNode = path[0];
	}

	private void Update()
	{
		if (path == null || curNode == null)
			return;

		if (nodeIndex + 1 < path.Count)
		{
			if (transform.position == curNode.vPosition)
			{
				nodeIndex++;
				curNode = path[nodeIndex];
			}
			Vector3 targetPos = curNode.vPosition;
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
		}
		else
			HitCastle();
	}

	private void HitCastle()
	{
		Main.Instance.levelController.ReduceXp(damage);
		EnemyWavesController.Instance.DestroyEnemy(this);
	}

	public void WeaponHit(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Main.Instance.User.CoinsIncrease(Random.Range(data.RewardRange[0], data.RewardRange[1] + 1));
			EnemyWavesController.Instance.DestroyEnemy(this);
		}
	}
}
