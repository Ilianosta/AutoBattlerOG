using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private List<Stat> stats = new List<Stat>();
    public Stat GetStat(Stat.Type type)
    {
        Stat requiredStat = stats.FirstOrDefault(stat => stat.GetStat == type);
        return requiredStat;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GenerateModifier(Stat.Type.life, StatModifier.Type.add, 10);
        if (Input.GetKeyDown(KeyCode.B)) GenerateModifier(Stat.Type.life, StatModifier.Type.multiply, 10);
        if (Input.GetKeyDown(KeyCode.C)) GenerateModifier(Stat.Type.life, StatModifier.Type.multiply, 10, 5);
        if (Input.GetKeyDown(KeyCode.D)) GenerateModifier(Stat.Type.life, StatModifier.Type.add, 4, 10);
    }

    private void GenerateModifier(Stat.Type statType, StatModifier.Type modType, float amount, float duration = -1)
    {
        StatModifier statModifier = new StatModifier(statType, amount, modType, duration);

        Stat stat = GetStat(statModifier.type);
        stat.AddModifier(statModifier);
        if (statModifier.duration > 0)
        {
            StartCoroutine(RemoveModifierIn(statModifier, statModifier.duration));
        }
    }
    private void GenerateModifier(StatModifier statModifier)
    {
        Stat stat = GetStat(statModifier.type);
        stat.AddModifier(statModifier);
        if (statModifier.duration > 0)
        {
            StartCoroutine("RemoveModifierIn", (statModifier, statModifier.duration));
        }
    }

    [ContextMenu("Create base stats")]
    private void SetBaseStats()
    {
        stats.Clear();
        foreach (Stat.Type type in Enum.GetValues(typeof(Stat.Type)))
        {
            stats.Add(new Stat(type));
        }
    }

    IEnumerator RemoveModifierIn(StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        GetStat(statModifier.type).RemoveModifier(statModifier);
    }
}
