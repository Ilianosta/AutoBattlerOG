using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private UI_CharacterSprite[] charSprites;
    [SerializeField] private GameObject charSpritePrefab;
    [SerializeField] private Transform velocityLine;
    [SerializeField] private RectTransform velocityPointA;
    [SerializeField] private RectTransform velocityPointB;

    private List<VelocityChar> velocityChars = new List<VelocityChar>();

    private void Awake()
    {
        if (UIManager.instance) Destroy(this);
        else UIManager.instance = this;
    }

    public void CreateCharSpriteInVelocity(Sprite sprite, float speed)
    {
        GameObject prefab = Instantiate(charSpritePrefab, velocityLine);
        prefab.GetComponent<Image>().sprite = sprite;

        RectTransform rectPrefab = prefab.GetComponent<RectTransform>();
        rectPrefab.position = velocityPointA.position;

        velocityChars.Add(new VelocityChar(rectPrefab, speed * GameManager.instance.speedMultiplierRule));
    }

    public void StartVelocityCounter()
    {
        Debug.Log("Velocity started");
        foreach (VelocityChar velocityChar in velocityChars)
        {
            Debug.Log("Char starting at: " + velocityChar.speed);
            velocityChar.rect.DOLocalMove(velocityPointB.localPosition, velocityChar.speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                PauseVelocityCounter();
            });
        }
    }

    public void PauseVelocityCounter()
    {
        foreach (VelocityChar velocityChar in velocityChars)
        {
            velocityChar.rect.DOKill();
        }
    }

    private class VelocityChar
    {
        public RectTransform rect;
        public float speed;
        public VelocityChar(RectTransform rect, float speed)
        {
            this.rect = rect;
            this.speed = speed;
        }
    }
}
