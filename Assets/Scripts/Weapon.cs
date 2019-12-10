using UnityEngine;

public class Weapon : MonoBehaviour
{
	private int damage;
	private float speed;
	private Enemy target;
	private Tower parentTower;

	public void CreateWeapon(Sprite icon, int dmg, float weapon_speed, Enemy curTarget, Tower tower)
	{
		GetComponent<SpriteRenderer>().sprite = icon;
		damage = dmg;
		speed = weapon_speed;
		target = curTarget;
		parentTower = tower;
	}

	private void Update()
	{
		if (target != null)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

			if (!parentTower.rangeNodes.Contains(target.CurNode))
			{
				if (parentTower.currentTarget != null)
					target = parentTower.currentTarget;
				else
					Destroy(gameObject);
			}
		}
		else
			Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.GetComponent<Enemy>() == target)
		{
			target.WeaponHit(damage);
			Destroy(gameObject);
		}
	}
}
