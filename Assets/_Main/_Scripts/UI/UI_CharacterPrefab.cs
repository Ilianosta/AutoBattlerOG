using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterPrefab : MonoBehaviour
{
    public UI_CharacterSelection uiCharacterSelections;
    public CharacterSO character;
    public bool selectable;
    [SerializeField] private Image image;

    private CharacterSO selectedCharacter;

    #region  Events
    public delegate void OnSelectCharacter(CharacterSO character);
    public static event OnSelectCharacter onSelectCharacter;

    public delegate void OnSelectCharacterAttach(UI_CharacterPrefab charPrefab);
    public static event OnSelectCharacterAttach onSelectCharacterAttach;
    #endregion
    private void Awake()
    {
        onSelectCharacter += c => selectedCharacter = c;
        if (selectable)
        {
            GetComponent<Button>().onClick.AddListener(() => SelectCharacter());
        }

        if (!selectable)
        {
            onSelectCharacterAttach += c => SelectCharacterAttach(c);
            GetComponent<Button>().onClick.AddListener(() => AttachCharacter());
        }
    }

    public void SetCharacterSprite()
    {
        image.sprite = character.sprite;
        image.gameObject.SetActive(true);
    }

    public void SelectCharacter()
    {
        if (!selectable) return;

        onSelectCharacter.Invoke(character);
    }

    public void AttachCharacter()
    {
        onSelectCharacterAttach.Invoke(this);
    }

    private void SelectCharacterAttach(UI_CharacterPrefab charPrefab)
    {
        if (!selectedCharacter) return;

        if (charPrefab == this)
        {
            character = selectedCharacter;
            SetCharacterSprite();
        }

        selectedCharacter = null;
        uiCharacterSelections.OnAttachCharacter(this);
    }
}
