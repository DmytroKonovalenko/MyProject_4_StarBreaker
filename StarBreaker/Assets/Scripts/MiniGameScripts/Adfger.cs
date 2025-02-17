using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adfger : MonoBehaviour
{
    public SpriteRenderer levelSpriteRenderer;

    private void Start()
    {
        LoadSelectedLevel();
    }

    private void LoadSelectedLevel()
    {
        // Завантажуємо індекс рівня та ім'я спрайту з PlayerPrefs
        int selectedLevelIndex = PlayerPrefs.GetInt("SelectedLevelIndex");
        string spriteName = PlayerPrefs.GetString("SelectedLevelSprite");

        // Отримуємо спрайт за ім'ям
        Sprite selectedSprite = Resources.Load<Sprite>("Sprites/" + spriteName);

        // Встановлюємо спрайт у SpriteRenderer
        if (selectedSprite != null && levelSpriteRenderer != null)
        {
            levelSpriteRenderer.sprite = selectedSprite;
        }
        else
        {
            Debug.LogError("Спрайт не знайдено або SpriteRenderer не налаштований!");
        }
    }
}
