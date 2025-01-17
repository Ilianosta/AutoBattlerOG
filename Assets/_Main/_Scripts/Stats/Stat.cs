using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public Stat(Type type, float value = 10)
    {
        this.type = type;
        this.statValue = value;
        this.modifiers = new List<StatModifier>();
    }
    public Stat(Stat stat)
    {
        this.type = stat.type;
        this.statValue = stat.statValue;
        this.modifiers = new List<StatModifier>();
    }

    [System.Serializable]
    public enum Type
    {
        life,
        armor,
        attack,
        speed
    }

    // Typing
    [SerializeField] private Type type;
    public Type GetStat => type;

    // Values
    [SerializeField] private float statValue;
    public float GetValue
    {
        get
        {
            if (modifiers == null) modifiers = new List<StatModifier>();

            float totalValue = statValue;
            foreach (StatModifier modifier in modifiers)
            {
                if (modifier.modifierType == StatModifier.Type.add) totalValue += modifier.amount;
                if (modifier.modifierType == StatModifier.Type.multiply) totalValue *= modifier.amount;
            }
            return totalValue;
        }
    }

    // Mods
    private List<StatModifier> modifiers = new List<StatModifier>();

    public void AddModifier(StatModifier statModifier)
    {
        if (modifiers == null) modifiers = new List<StatModifier>();
        if (statModifier.amount != 0) modifiers.Add(statModifier);
        SortList();
    }
    public void RemoveModifier(StatModifier statModifier)
    {
        if (modifiers == null) modifiers = new List<StatModifier>();
        if (statModifier.amount != 0) modifiers.Remove(statModifier);
        SortList();
    }

    private void SortList()
    {
        var sortedList = modifiers.OrderBy(x => x.modifierType == StatModifier.Type.multiply).ToList();
        modifiers = sortedList;
    }
}