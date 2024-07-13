using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        Animate(hpBar, percentage);
        if (percentage <= 0) charSprite.color = Color.gray;
        else charSprite.color = Color.white;
    }

    public void SetManaPercentage(float percentage)
    {
        Animate(manaBar, percentage);
    }

    public void SetCharacterSprite(Sprite sprite)
    {
        charSprite.sprite = sprite;
    }

    private void Animate(Image image, float to)
    {
        Sequence s = DOTween.Sequence();
        float from = image.fillAmount;
        s.AppendInterval(0.2f).SetEase(Ease.OutQuart).OnUpdate(() =>
        {
            image.fillAmount = Mathf.Lerp(from, to, s.Elapsed() / s.Duration());
        });
    }
}
