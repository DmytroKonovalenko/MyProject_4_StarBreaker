using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> goldTexts;

    private void Start()
    {
        UpdateGoldTexts(ZolotoManager.Instance.GetCurrentGold());
        ZolotoManager.Instance.OnGoldChanged += UpdateGoldTexts;

        ZolotoManager.Instance.LoadZoloto();
        UpdateGoldTexts(ZolotoManager.Instance.zoloto);
    }

    private void OnDestroy()
    {
        ZolotoManager.Instance.OnGoldChanged -= UpdateGoldTexts;
    }

    private void UpdateGoldTexts(int amount)
    {
        foreach (var text in goldTexts)
        {
            text.text = amount.ToString();
        }
    }
}
