using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool GamePaused;
    public float speedMultiplierRule;
    private bool activeTurn = false;
    public bool ActiveTurn
    {
        get { return activeTurn; }
        set
        {
            activeTurn = value;
            if (activeTurn) UIManager.instance.PauseVelocityCounter();
            else UIManager.instance.StartVelocityCounter();
        }
    }

    private void Awake()
    {
        if (GameManager.instance) Destroy(this);
        else GameManager.instance = this;
    }
}
