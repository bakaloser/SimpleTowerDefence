using UnityEngine;
using System.Collections;
using TMPro;

public class Message : MonoBehaviour
{
	public TextMeshProUGUI text;

	public void ShowGOMsg()
	{
		gameObject.SetActive(true);
		text.text = "Game over";
	}
	public void ShowWinMsg()
	{
		gameObject.SetActive(true);
		text.text = "Level success";
	}

	public void OnClick()
	{
		if (gameObject.activeInHierarchy)
		{
			gameObject.SetActive(false);
			Main.Instance.RestartLevel();
		}
	}
}
