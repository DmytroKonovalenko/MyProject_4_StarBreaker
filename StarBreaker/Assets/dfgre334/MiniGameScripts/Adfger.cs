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
        // ����������� ������ ���� �� ��'� ������� � PlayerPrefs
        int selectedLevelIndex = PlayerPrefs.GetInt("SelectedLevelIndex");
        string spriteName = PlayerPrefs.GetString("SelectedLevelSprite");

        // �������� ������ �� ��'��
        Sprite selectedSprite = Resources.Load<Sprite>("Sprites/" + spriteName);

        // ������������ ������ � SpriteRenderer
        if (selectedSprite != null && levelSpriteRenderer != null)
        {
            levelSpriteRenderer.sprite = selectedSprite;
        }
        else
        {
            Debug.LogError("������ �� �������� ��� SpriteRenderer �� ������������!");
        }
    }
}
