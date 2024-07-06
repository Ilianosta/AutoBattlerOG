using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CharacterTargetSystem
{
    public CharacterTargetSystem(Skill mySelf)
    {
        this.mySelf = mySelf;
    }

    [SerializeField] private int amountOfTargets;
    [SerializeField] private Skill.SkillTarget targetType;
    [SerializeField] private bool affectAllies;
    [SerializeField] private bool affectEnemies;
    private Skill mySelf;
    public List<CharacterController> GetTargets()
    {
        List<CharacterController> charTargets = targetType switch
        {
            Skill.SkillTarget.none => GetTargetDefault(),
            Skill.SkillTarget.myself => GetTargetMySelf(),
            Skill.SkillTarget.minHp => GetTargetsByHp(),
            Skill.SkillTarget.maxHp => GetTargetsByHp(),
            Skill.SkillTarget.maxAtk => GetTargetsByAtk(),
            Skill.SkillTarget.minAtk => GetTargetsByAtk(),
            Skill.SkillTarget.maxRange => GetTargetsByRange(),
            Skill.SkillTarget.minRange => GetTargetsByRange(),
            Skill.SkillTarget.all => GetTargetAll(),
            _ => new List<CharacterController>()
        };

        return charTargets;
    }

    private List<CharacterController> GetTargetAll()
    {
        return GameManager.instance.charactersInGame.Where(c => !c.IsDead).ToList();
    }


    private List<CharacterController> GetTargetsByAtk()
    {
        List<CharacterController> characters = new List<CharacterController>();
        return targetType switch
        {
            Skill.SkillTarget.minAtk => characters.OrderBy(c => c.Stats.GetStat(Stat.Type.attack)).ToList(),
            Skill.SkillTarget.maxAtk => characters.OrderByDescending(c => c.Stats.GetStat(Stat.Type.attack)).ToList(),
            _ => characters
        };
    }


    private List<CharacterController> GetTargetsByRange()
    {
        List<CharacterController> charAllies = new List<CharacterController>();
        List<CharacterController> charEnemies = new List<CharacterController>();

        if (affectAllies)
        {
            charAllies.AddRange(GetEnemiesOrAllies(true));
            RemoveMeelesFromList(charAllies);
        }
        if (affectEnemies)
        {
            charEnemies.AddRange(GetEnemiesOrAllies(false));
            RemoveMeelesFromList(charEnemies);
        }
        List<CharacterController> characters = new List<CharacterController>();

        if (affectAllies && affectEnemies)
        {
            characters.AddRange(charAllies.Where(c => !c.IsDead).Take(1).ToList());
            characters.AddRange(charEnemies.Where(c => !c.IsDead).Take(1).ToList());
        }

        if (affectAllies) characters.AddRange(charAllies.Where(c => !c.IsDead).Take(1).ToList());
        if (affectEnemies) characters.AddRange(charEnemies.Where(c => !c.IsDead).Take(1).ToList());

        return characters;
    }
    private List<CharacterController> RemoveMeelesFromList(List<CharacterController> characters)
    {
        foreach (CharacterController character in characters)
        {
            bool remove = character.id switch
            {
                0 => true,
                1 => true,
                8 => true,
                9 => true,
                _ => false
            };
            if (remove) characters.RemoveAt(character.id);
        }
        return characters;
    }
    private List<CharacterController> GetTargetMySelf()
    {
        return new List<CharacterController> { mySelf.GetComponent<CharacterController>() };
    }


    private List<CharacterController> GetTargetDefault()
    {
        List<CharacterController> characters = GetEnemiesOrAllies(false);
        return characters
            .Where(c => !c.IsDead)
            .Take(1)
            .ToList();
    }

    private List<CharacterController> GetTargetsByHp()
    {
        List<CharacterController> characters = new List<CharacterController>();

        if (affectAllies) characters.AddRange(GetEnemiesOrAllies(true));
        if (affectEnemies) characters.AddRange(GetEnemiesOrAllies(false));

        return targetType switch
        {
            Skill.SkillTarget.minHp => characters.OrderBy(c => c.ActualLife).ToList(),
            Skill.SkillTarget.maxHp => characters.OrderByDescending(c => c.ActualLife).ToList(),
            _ => characters
        };
    }

    private List<CharacterController> GetEnemiesOrAllies(bool isAlly)
    {
        int startIndex = isAlly ? 0 : 5;
        int endIndex = isAlly ? 4 : GameManager.instance.charactersInGame.Count;

        return GameManager.instance.charactersInGame
            .Skip(startIndex)
            .Take(endIndex - startIndex)
            .Where(c => !c.IsDead)
            .ToList();
    }
}