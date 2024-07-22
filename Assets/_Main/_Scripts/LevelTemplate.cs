using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTemplate", menuName = "Game/LevelTemplate", order = 0)]
public class LevelTemplate : ScriptableObject
{
    [SerializeField] public Stage[] stages;
    
    [System.Serializable]
    public class Stage
    {
        public List<CharacterSelection> characterSelections;
    }
}

