using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private float velocityDistance = 100;
    private List<VelocityChar> velocityChars = new List<VelocityChar>();

    private void Awake()
    {
        if (UIManager.instance) Destroy(this);
        else UIManager.instance = this;
    }

    public void CreateCharSpriteInVelocity(int id, Sprite sprite, float speed)
    {
        GameObject prefab = Instantiate(charSpritePrefab, velocityLine);
        prefab.GetComponent<Image>().sprite = sprite;

        RectTransform rectPrefab = prefab.GetComponent<RectTransform>();
        rectPrefab.localPosition = velocityPointA.localPosition;

        velocityChars.Add(new VelocityChar(id, rectPrefab, speed * GameManager.instance.speedMultiplierRule));
    }

    public void UpdateCharStatusInUI(int id, Sprite sprite, float hpPercentage = 100, float manaPercentage = 0)
    {
        var charSprite = charSprites[id];

        if (!charSprite.gameObject.activeSelf) charSprite.gameObject.SetActive(true);

        charSprite.id = id;
        charSprite.SetCharacterSprite(sprite);
        charSprite.SetHpPercentage(hpPercentage);
        charSprite.SetManaPercentage(manaPercentage);
    }

    public void UpdateCharVelocityIcon(int id)
    {
        VelocityChar icon = velocityChars.FirstOrDefault(icon => icon.id == id);
        icon.actualPosition += icon.speed;
        if (icon.actualPosition > velocityDistance)
        {
            icon.actualPosition = velocityDistance;
            GameManager.instance.activeTurn = true;
        }
        float newPosition = icon.actualPosition / velocityDistance;
        icon.rect.localPosition = Vector3.LerpUnclamped(velocityPointA.localPosition, velocityPointB.localPosition, newPosition);
    }

    [System.Serializable]
    private class VelocityChar
    {
        public int id;
        public RectTransform rect;
        public float speed;
        public float actualPosition = 0;
        public VelocityChar(int id, RectTransform rect, float speed)
        {
            this.id = id;
            this.rect = rect;
            this.speed = speed;
            this.actualPosition = 0;
        }
    }
}
