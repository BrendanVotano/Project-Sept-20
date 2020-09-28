using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int score = 0;

    public void Score (int _amount)
    {
        score += _amount;
    }
}
