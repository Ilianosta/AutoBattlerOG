using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool GamePaused = false;
    public float speedMultiplierRule;
    public Transform[] charPositions;
    public List<CharacterController> charactersInGame = new List<CharacterController>();
    public List<CharacterController> Allies
    {
        get
        {
            int startIndex = 0;
            int endIndex = 5;
            return GameManager.instance.charactersInGame
                       .Skip(startIndex)
                       .Take(endIndex - startIndex)
                       .Where(c => !c.IsDead)
                       .ToList();
        }
    }
    public List<CharacterController> Enemies
    {
        get
        {
            int startIndex = 5;
            int endIndex = charactersInGame.Count;
            return GameManager.instance.charactersInGame
                       .Skip(startIndex)
                       .Take(endIndex - startIndex)
                       .Where(c => !c.IsDead)
                       .ToList();
        }
    }
    public delegate void OnCharacterCreated();
    public event OnCharacterCreated onCharacterCreated;
    [SerializeField] private GameObject turnObject;
    private void Awake()
    {
        if (GameManager.instance) Destroy(this);
        else GameManager.instance = this;

        CharacterController.onCharacterTurn += OnCharacterTurn;
    }

    private void Start()
    {
        foreach (CharacterSelection selection in CharacterSelections.instance.selections)
        {
            GameObject newCharacter = Instantiate(selection.character.model, charPositions[selection.position]);
            var newCharController = newCharacter.GetComponent<CharacterController>();
            newCharController.id = selection.position;
            charactersInGame.Add(newCharController);
        }
    }

    public void _OnCharacterCreated()
    {

    }

    private void OnCharacterTurn(bool start, int id)
    {
        turnObject.SetActive(start);
        Vector3 objPosition = charactersInGame.FirstOrDefault((character) => character.id == id).transform.position;
        objPosition.y += 3;
        turnObject.transform.position = objPosition;
    }
}
