using System;

public class CoinsManager : SingletonOfScene<CoinsManager>
{
    private int coins = 0;
    public int Coins
    {
        get => coins; 
        
        set
        {
            var oldValue = coins;
            coins = value;
            if (coins != oldValue && onChangeCoins != null) {
                onChangeCoins.Invoke();
            }
        }
    }
    private Action onChangeCoins;

    public void AddOnChangeCoinsListener(Action onChangeCoins) {
        this.onChangeCoins = onChangeCoins;
    }
}
