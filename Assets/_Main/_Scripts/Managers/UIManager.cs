using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject winScreeObj;
    [SerializeField] private GameObject gameUIObj;
    // -----------------------------------------------------------
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

        ShowGameUI(true);
        ShowWinScreen(false);
    }

    public void ShowWinScreen(bool show)
    {
        winScreeObj.SetActive(show);
    }

    public void ShowGameUI(bool show)
    {
        gameUIObj.SetActive(show);
    }

    public void CreateCharSpriteInVelocity(int id, Sprite sprite, float speed)
    {
        GameObject prefab = Instantiate(charSpritePrefab, velocityLine);
        prefab.GetComponent<Image>().sprite = sprite;

        RectTransform rectPrefab = prefab.GetComponent<RectTransform>();
        rectPrefab.localPosition = velocityPointA.localPosition;
        float rndGap = ((speed * Random.Range(-20, 20)) / 100);
        speed += rndGap;
        velocityChars.Add(new VelocityChar(id, rectPrefab, speed * GameManager.instance.speedMultiplierRule));
    }

    public void UpdateCharStatusInUI(int id, float hpPercentage = 100, float manaPercentage = 0, Sprite sprite = null)
    {
        if (id > 4) return;

        var charSprite = charSprites[id];

        if (!charSprite.gameObject.activeSelf) charSprite.gameObject.SetActive(true);

        charSprite.id = id;
        charSprite.SetHpPercentage(hpPercentage);
        charSprite.SetManaPercentage(manaPercentage);
        if (sprite) charSprite.SetCharacterSprite(sprite);
    }

    public bool UpdateCharVelocityIcon(int id)
    {
        bool isMyTurn = false;
        VelocityChar icon = velocityChars.FirstOrDefault(icon => icon.id == id);
        icon.actualPosition += icon.speed * Time.deltaTime;
        // Debug.Log("icon speed: " + icon.speed);
        if (icon.actualPosition > velocityDistance)
        {
            // Debug.Log("TURN ACTIVATED - actual pos | velocity: " + icon.actualPosition + " | " + velocityDistance);
            icon.actualPosition = 0;
            isMyTurn = true;
        }
        float newPosition = icon.actualPosition / velocityDistance;
        // Debug.Log("newposition: " + newPosition);
        icon.rect.localPosition = Vector3.LerpUnclamped(velocityPointA.localPosition, velocityPointB.localPosition, newPosition);
        return isMyTurn;
    }

    public void EnableCharVelocityIcon(bool enable, int id)
    {
        VelocityChar icon = velocityChars.FirstOrDefault(icon => icon.id == id);
        icon.actualPosition = 0;
        icon.rect.gameObject.SetActive(enable);
    }

    public void ResetCharVelocities()
    {
        foreach (VelocityChar icon in velocityChars)
        {
            icon.actualPosition = 0;
            icon.rect.localPosition = Vector3.LerpUnclamped(velocityPointA.localPosition, velocityPointB.localPosition, 0);
        }
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
