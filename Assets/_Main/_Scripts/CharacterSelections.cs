using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
[System.Serializable]
public class CharacterSelection
{
    public CharacterSO character;
    public int position;
}
