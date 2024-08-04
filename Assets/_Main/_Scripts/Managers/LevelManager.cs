using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private LevelTemplate[] levelTemplates;
    [SerializeField] private GameObject[] levelMapPrefabs;

    private int selectedTemplateLevel = 0;
    private int selectedMapLevel = 0;
    private int actualStage;
    
    // Props
    public LevelTemplate actualLevelTemplate => levelTemplates[selectedTemplateLevel];
    public LevelTemplate.Stage actualLevelTemplateStage => levelTemplates[selectedTemplateLevel].stages[actualStage];

    private void Awake()
    {
        if (LevelManager.instance) Destroy(this);
        else LevelManager.instance = this;
    }

    public void AleatorizeNewLevel()
    {
        selectedTemplateLevel = Random.Range(0, levelTemplates.Length);
        selectedMapLevel = Random.Range(0, levelMapPrefabs.Length);
    }

    public void SelectLevel(int template, int map)
    {
        selectedTemplateLevel = template;
        selectedMapLevel = map;
    }

    public void InstantiateNewLevel()
    {
        if (levelMapPrefabs.Length > 0) Instantiate(levelMapPrefabs[selectedMapLevel]);
    }

    public bool GoToNextStage()
    {
        actualStage++;
        int maxStages = levelTemplates[selectedTemplateLevel].stages.Length - 1;
        if (actualStage > maxStages)
        {
            actualStage = maxStages;
            return false;
        }
        return true;
    }
    public void GoToPreviousStage()
    {
        actualStage--;
        if (actualStage < 0) actualStage = 0;
    }
}
