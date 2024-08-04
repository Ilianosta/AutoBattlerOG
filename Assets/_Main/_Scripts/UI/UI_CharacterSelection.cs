using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UI_CharacterSelection : MonoBehaviour
{
    [SerializeField] private List<CharacterSO> characterList;
    [SerializeField] private Transform characterListParent;
    [SerializeField] private UI_CharacterPrefab uiCharacterPrefab;
    [SerializeField] private UI_CharacterPrefab[] uiSelectedCharacters;
    [SerializeField] private GameObject blackScreen;

    private void Awake()
    {
        CreateCharacterList();
        UI_CharacterPrefab.onSelectCharacter += c => OnSelectCharacter(c);
    }

    private void CreateCharacterList()
    {
        foreach (CharacterSO character in characterList)
        {
            GameObject newChar = Instantiate(uiCharacterPrefab.gameObject, characterListParent);
            newChar.GetComponent<UI_CharacterPrefab>().character = character;
            newChar.GetComponent<UI_CharacterPrefab>().SetCharacterSprite();
            newChar.GetComponent<UI_CharacterPrefab>().uiCharacterSelections = this;
        }
    }

    private void OnSelectCharacter(CharacterSO character)
    {
        ShowBlackScreen(true);
    }

    public void OnAttachCharacter(UI_CharacterPrefab characterPrefab)
    {
        ShowBlackScreen(false);
        for (int i = 0; i < uiSelectedCharacters.Length; i++)
        {
            if (uiSelectedCharacters[i] == characterPrefab)
            {
                CharacterSelections.instance.SelectCharacter(uiSelectedCharacters[i].character, i);
                break;
            }
        }
    }

    private void ShowBlackScreen(bool show)
    {
        blackScreen.SetActive(show);
    }

    public void SetSelectedCharacter(CharacterSO character, int position)
    {
        uiSelectedCharacters[position].character = character;
    }
}
