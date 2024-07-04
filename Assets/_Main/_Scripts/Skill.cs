using UnityEngine;
public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float amount;
    [SerializeField] protected SkillObjective objective;
    [SerializeField] protected bool affectAllies;
    [SerializeField] protected bool affectEnemies;
    [SerializeField] protected Animator animator;

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
