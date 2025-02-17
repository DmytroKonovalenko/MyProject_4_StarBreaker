using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    [SerializeField] private List<Image> dayImages;
    [SerializeField] private Sprite todaySprite;
    [SerializeField] private Sprite claimedSprite;
    [SerializeField] private Sprite upcomingSprite;
    [SerializeField] private Button claimButton;
    [SerializeField] private int[] dailyCrystalRewards;

    private const string LastClaimedDayKey = "LastClaimedDay";
    private const string LastClaimedDateKey = "LastClaimedDate";
    private ZolotoManager zolotoManager;

    private void Start()
    {
        zolotoManager = FindObjectOfType<ZolotoManager>();
        UpdateDayVisuals();
        claimButton.onClick.AddListener(ClaimReward);
    }

    private void UpdateDayVisuals()
    {
        int lastClaimedDay = PlayerPrefs.GetInt(LastClaimedDayKey, -1);
        string lastClaimedDateStr = PlayerPrefs.GetString(LastClaimedDateKey, string.Empty);
        DateTime lastClaimedDate = string.IsNullOrEmpty(lastClaimedDateStr) ? DateTime.MinValue : DateTime.Parse(lastClaimedDateStr);

        if ((DateTime.Now - lastClaimedDate).Days > 0 && lastClaimedDay < dayImages.Count - 1)
        {
            PlayerPrefs.SetInt(LastClaimedDayKey, lastClaimedDay + 1);
            PlayerPrefs.Save();
        }

        lastClaimedDay = PlayerPrefs.GetInt(LastClaimedDayKey, -1);

        for (int i = 0; i < dayImages.Count; i++)
        {
            if (i < lastClaimedDay)
            {
                dayImages[i].sprite = claimedSprite;
            }
            else if (i == lastClaimedDay)
            {
                dayImages[i].sprite = todaySprite;
            }
            else
            {
                dayImages[i].sprite = upcomingSprite;
            }
        }

        claimButton.interactable = lastClaimedDay < dayImages.Count && (DateTime.Now - lastClaimedDate).Days > 0;
    }

    private void ClaimReward()
    {
        int currentDay = PlayerPrefs.GetInt(LastClaimedDayKey, -1);

        if (currentDay < 0 || currentDay >= dailyCrystalRewards.Length) return;

        zolotoManager.AddZoloto(dailyCrystalRewards[currentDay]);

        PlayerPrefs.SetString(LastClaimedDateKey, DateTime.Now.ToString());
        PlayerPrefs.SetInt(LastClaimedDayKey, currentDay + 1);
        PlayerPrefs.Save();

        UpdateDayVisuals();
    }

}
