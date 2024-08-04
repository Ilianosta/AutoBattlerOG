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
    [SerializeField] private GameObject turnObject;
    // Props
    public List<CharacterController> Allies
    {
        get
        {
            int startIndex = 0;
            int endIndex = 4;
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

    // Events
    public delegate void OnCharacterDead(CharacterController character);
    public OnCharacterDead onCharacterDead;
    private void Awake()
    {
        if (GameManager.instance) Destroy(this);
        else GameManager.instance = this;

        CharacterController.onCharacterTurn += OnCharacterTurn;
        onCharacterDead += CharacterDead;
    }

    private void Start()
    {
        LevelManager.instance.AleatorizeNewLevel();
        LevelManager.instance.InstantiateNewLevel();

        InstantiateCharacters(CharacterSelections.instance.selections, true);
        InstantiateCharacters(LevelManager.instance.actualLevelTemplateStage.characterSelections, false);
    }

    private void CharacterDead(CharacterController character)
    {
        UIManager.instance.EnableCharVelocityIcon(false, character.id);

        bool allEnemiesDead = true;

        for (int i = 5; i < charactersInGame.Count; i++)
        {
            if (!charactersInGame[i].IsDead) allEnemiesDead = false;
        }

        if (allEnemiesDead) EndStage();
    }

    private void InstantiateCharacters(List<CharacterSelection> selections, bool isAlly)
    {
        foreach (CharacterSelection selection in selections)
        {
            if (selection.character == null) continue;
            
            int selectionPos = isAlly ? selection.position : selection.position + 5;
            Transform charPos = charPositions[selectionPos];
            GameObject newCharacter = Instantiate(selection.character.model, charPos.position, charPos.rotation, charPos);
            var newCharController = newCharacter.GetComponent<CharacterController>();
            newCharController.id = selectionPos;
            charactersInGame.Add(newCharController);
        }
    }

    private void OnCharacterTurn(bool start, int id)
    {
        turnObject.SetActive(start);
        Vector3 objPosition = charactersInGame.FirstOrDefault((character) => character.id == id).transform.position;
        objPosition.y += 3;
        turnObject.transform.position = objPosition;
    }

    public void EndStage()
    {
        GameManager.GamePaused = true;
        for (int i = 5; i < charactersInGame.Count; i++)
        {
            charactersInGame[i].gameObject.SetActive(false);
            charactersInGame.Remove(charactersInGame[i]);
        }
        UIManager.instance.ResetCharVelocities();

        // TODO MOVE CHARACTERS ALONG THE MAP

        if (LevelManager.instance.GoToNextStage())
        {
            InstantiateCharacters(LevelManager.instance.actualLevelTemplateStage.characterSelections, false);
            GameManager.GamePaused = false;
        }
        else
        {
            EndGame(true);
        }
    }

    public void EndGame(bool win)
    {
        UIManager.instance.ShowGameUI(false);
        UIManager.instance.ShowWinScreen(true);
    }
}
