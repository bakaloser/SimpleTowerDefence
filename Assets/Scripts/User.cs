public class User
{
	private int coins = 100;
	public int Coins
	{
		get
		{
			return coins;
		}
	}

	public User()
	{
		HUD.Instance.UpdateCoinsCount(coins);
	}

	public void CoinsReduce(int count)
	{
		coins -= count;
		HUD.Instance.UpdateCoinsCount(coins);
	}
	public void CoinsIncrease(int count)
	{
		coins += count;
		HUD.Instance.UpdateCoinsCount(coins);
	}

	public void ResetAll()
	{
		coins = 100;
		HUD.Instance.UpdateCoinsCount(coins);
	}
}