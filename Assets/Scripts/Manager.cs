using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    int playerCoins = 0;

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        Debug.Log("You have " + playerCoins + " coins.");
    }
}
