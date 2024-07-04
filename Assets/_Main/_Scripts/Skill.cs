using UnityEngine;
public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private SkillObjective objective;
    [SerializeField] private bool affectAllies;
    [SerializeField] private bool affectEnemies;
    public abstract void Cast();

    public enum SkillObjective
    {
        none,
        myself,
        minHp,
        maxHp,
        maxAtk,
        minAtk,
        maxRange,
        minRange,
        all
    }
}
