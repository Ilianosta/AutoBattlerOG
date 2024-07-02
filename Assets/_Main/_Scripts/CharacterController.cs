using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterSO character;
    [SerializeField] private CharacterStats stats;

    private void Start()
    {
        stats = new CharacterStats(character.stats);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GenerateModifier(Stat.Type.life, StatModifier.Type.add, 10);
        if (Input.GetKeyDown(KeyCode.B)) GenerateModifier(Stat.Type.life, StatModifier.Type.multiply, 10);
        if (Input.GetKeyDown(KeyCode.C)) GenerateModifier(Stat.Type.life, StatModifier.Type.multiply, 10, 5);
        if (Input.GetKeyDown(KeyCode.D)) GenerateModifier(Stat.Type.life, StatModifier.Type.add, 4, 10);
    }
    #region "Utilities"
    private void GenerateModifier(Stat.Type statType, StatModifier.Type modType, float amount, float duration = -1)
    {
        StatModifier statModifier = new StatModifier(statType, amount, modType, duration);

        Stat stat = stats.GetStat(statModifier.type);
        stat.AddModifier(statModifier);
        if (statModifier.duration > 0)
        {
            StartCoroutine(RemoveModifierIn(statModifier, statModifier.duration));
        }
    }
    private void GenerateModifier(StatModifier statModifier)
    {
        Stat stat = stats.GetStat(statModifier.type);
        stat.AddModifier(statModifier);
        if (statModifier.duration > 0)
        {
            StartCoroutine("RemoveModifierIn", (statModifier, statModifier.duration));
        }
    }
    #endregion
    private void CreateCharacter()
    {
        UIManager.instance.CreateCharSpriteInVelocity(character.sprite, stats.GetStat(Stat.Type.speed).GetValue);
    }

    public void Cast()
    {

    }

    public void TakeDamage(float amount)
    {
        amount -= stats.GetStat(Stat.Type.armor).GetValue;
        if (amount > 0) stats.ActualLife -= amount;
    }

    IEnumerator RemoveModifierIn(StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        stats.GetStat(statModifier.type).RemoveModifier(statModifier);
    }
}
