using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSprite : MonoBehaviour
{
    public int id;
    [SerializeField] private Image charSprite;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image manaBar;

    public void SetHpPercentage(float percentage)
    {
        hpBar.fillAmount = percentage;
    }

    public void SetManaPercentage(float percentage)
    {
        manaBar.fillAmount = percentage;
    }

    public void SetCharacterSprite(Sprite sprite)
    {
        charSprite.sprite = sprite;
    }
}
