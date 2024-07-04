using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool GamePaused = true;
    public float speedMultiplierRule;
    public bool activeTurn = false;
    public Transform[] charPositions;
    public delegate void OnCharacterCreated();
    public event OnCharacterCreated onCharacterCreated;

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
            newCharacter.GetComponent<CharacterController>().id = selection.position;
        }
    }

    public void _OnCharacterCreated()
    {

    }
}
