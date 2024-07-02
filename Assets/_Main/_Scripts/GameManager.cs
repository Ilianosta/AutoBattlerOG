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

    public Transform[] charPositions;
    private void Awake()
    {
        if (GameManager.instance) Destroy(this);
        else GameManager.instance = this;
    }

    private void Start()
    {
        foreach (CharacterSelection selection in CharacterSelections.instance.selections)
        {
            GameObject newCharacter = Instantiate(selection.character.model, charPositions[selection.position]);
        }
    }
}
