using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level", order = 51)]
public class LevelData : ScriptableObject
{
	[SerializeField]
	private int userXp;
	[SerializeField]
	private int wavesCount;

	public int XP => userXp;
	public int WavesCount => wavesCount;
}