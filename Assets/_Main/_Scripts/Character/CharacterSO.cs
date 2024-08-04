using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterName", menuName = "Game/Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    public GameObject model;
    public Sprite sprite;
    public List<Stat> stats = new List<Stat>();
    public Stat GetStat(Stat.Type type)
    {
        Stat requiredStat = stats.FirstOrDefault(stat => stat.GetStat == type);
        return requiredStat;
    }

    // UTILITY

    [ContextMenu("Create base stats")]
    private void SetBaseStats()
    {
        stats.Clear();
        foreach (Stat.Type type in Enum.GetValues(typeof(Stat.Type)))
        {
            stats.Add(new Stat(type));
        }
    }
}

