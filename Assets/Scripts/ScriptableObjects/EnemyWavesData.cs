using UnityEngine;


[CreateAssetMenu(fileName = "New EnemyWave", menuName = "EnemyWaves", order = 52)]
public class EnemyWavesData : ScriptableObject
{
	[SerializeField]
	private float duration;
	[SerializeField]
	private int enemys_count;
	[SerializeField]
	private EnemyData enemyType;

	public float WaveDuration => duration;
	public int EnemysCount => enemys_count;
	public EnemyData EnemyType => enemyType;
}
