using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSelections : MonoBehaviour
{
    public static CharacterSelections instance;
    public List<CharacterSelection> selections = new List<CharacterSelection>();
    private void Awake()
    {
        if (CharacterSelections.instance) Destroy(this);
        else CharacterSelections.instance = this;

        DontDestroyOnLoad(this);
    }

    public void SelectCharacter(CharacterSO character, int position)
    {
        selections[position] = new CharacterSelection(character, position);
    }
}
[System.Serializable]
public class CharacterSelection
{
    public CharacterSO character;
    public int position;

    public CharacterSelection(CharacterSO character, int position)
    {
        this.character = character;
        this.position = position;
    }
}
