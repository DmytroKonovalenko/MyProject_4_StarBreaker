using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterLevelManager : MonoBehaviour
{
    [Header("Current Level UI")]
    [SerializeField] private Image levelImage;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private List<TextMeshProUGUI> upgradeTexts;

    [Header("Selected Character UI")]
    [SerializeField] private Image selectedLevelImage;   
    [SerializeField] private TextMeshProUGUI selectedLevelNumberText;
    [SerializeField] private List<TextMeshProUGUI> selectedUpgradeTexts;
    [SerializeField] private List<Image> upgradeStatusImages;
    [SerializeField] private TextMeshProUGUI upgradeCounterText;
    [SerializeField] private Sprite purchasedSprite;
    [SerializeField] private Sprite notPurchasedSprite;

    [Header("Upgrade Price Texts")]
    [SerializeField] private List<TextMeshProUGUI> upgradePriceTexts;

    [Header("Buttons")]
    [SerializeField] private List<Button> upgradeButtons;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    [SerializeField] private CharacterLevel[] levels;

    private int currentLevelIndex = 0;
    private int selectedLevelIndex = -1;


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        LoadGameData();
        if (selectedLevelIndex == -1)
        {
            selectedLevelIndex = 0;
            SaveSelectedLevel();
        }
        UpdateLevelUI();
        UpdateSelectedLevelUI();
        nextButton.onClick.AddListener(NextLevel);
        prevButton.onClick.AddListener(PrevLevel);
        selectButton.onClick.AddListener(SelectLevel);

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int upgradeIndex = i;
            upgradeButtons[i].onClick.AddListener(() => BuyUpgrade(upgradeIndex));
        }
    }

    private void UpdateLevelUI()
    {
        CharacterLevel currentLevel = levels[currentLevelIndex];
        levelImage.DOFade(0f, 0.3f).OnComplete(() =>
        {
            levelImage.sprite = currentLevel.levelSprite;
            levelImage.DOFade(1f, 0.3f);
        });
        levelNameText.text = currentLevel.levelName;
        levelNumberText.text = "LVL: " + (currentLevelIndex + 1);
        for (int i = 0; i < upgradeTexts.Count; i++)
        {
            if (i < currentLevel.upgrades.Length) 
            {
                Upgrade upgrade = currentLevel.upgrades[i];
                upgradeTexts[i].text = upgrade.upgradeName;
                upgradeTexts[i].color = upgrade.isPurchased ? Color.green : Color.white;
               
            }
            else
            {
                upgradeTexts[i].text = string.Empty;              
            }
        }
        prevButton.interactable = currentLevelIndex > 0;
        nextButton.interactable = CanProceedToNextLevel();
        selectButton.gameObject.SetActive(currentLevelIndex != selectedLevelIndex);
    }
    private void UpdateSelectedLevelUI()
    {
        CharacterLevel selectedLevel = levels[selectedLevelIndex];

        selectedLevelImage.sprite = selectedLevel.levelSprite;
        
        selectedLevelNumberText.text = "LVL: " + (selectedLevelIndex + 1);

        int purchasedUpgrades = 0;

        for (int i = 0; i < selectedUpgradeTexts.Count; i++)
        {
            if (i < selectedLevel.upgrades.Length)
            {
                Upgrade upgrade = selectedLevel.upgrades[i];
                selectedUpgradeTexts[i].text = upgrade.upgradeName;
               

                if (i < upgradeStatusImages.Count)
                {
                    upgradeStatusImages[i].sprite = upgrade.isPurchased ? purchasedSprite : notPurchasedSprite;
                }

                if (upgrade.isPurchased)
                {
                    purchasedUpgrades++;
                }
            }
            else
            {
                selectedUpgradeTexts[i].text = string.Empty;

                if (i < upgradeStatusImages.Count)
                {
                    upgradeStatusImages[i].sprite = notPurchasedSprite;
                }
            }
        }
        int totalUpgrades = selectedLevel.upgrades.Length;
        upgradeCounterText.text = $"{purchasedUpgrades}/{totalUpgrades}";
        for (int i = 0; i < upgradeTexts.Count; i++)
        {
            if (i < selectedLevel.upgrades.Length) 
            {
                Upgrade upgrade = selectedLevel.upgrades[i];
                upgradeTexts[i].text = upgrade.upgradeName;
                upgradeTexts[i].color = upgrade.isPurchased ? Color.green : Color.white;
                if (i < upgradePriceTexts.Count && i < upgradeButtons.Count)
                {
                    bool isPurchased = upgrade.isPurchased;

                    upgradePriceTexts[i].text = isPurchased ? "Purchased" : $"{upgrade.upgradeCost}";
                    upgradePriceTexts[i].gameObject.SetActive(!isPurchased); 

                    upgradeButtons[i].gameObject.SetActive(!isPurchased); 
                }
            }
            else
            {
                upgradeTexts[i].text = string.Empty;
                if (i < upgradePriceTexts.Count && i < upgradeButtons.Count)
                {
                    upgradePriceTexts[i].gameObject.SetActive(false);
                    upgradeButtons[i].gameObject.SetActive(false);
                }
            }
        }                
    }
    private bool CanProceedToNextLevel()
    {
        CharacterLevel currentLevel = levels[currentLevelIndex];

        foreach (var upgrade in currentLevel.upgrades)
        {
            if (!upgrade.isPurchased)
            {
                return false; 
            }
        }

        return true; 
    }
    public void SetCurrentLevelUIFromSelected()
    {
        currentLevelIndex = selectedLevelIndex;
        UpdateLevelUI();
    }
    private void NextLevel()
    {
        if (currentLevelIndex < levels.Length - 1)
        {
            currentLevelIndex++;
            UpdateLevelUI();
        }
    }
    private void PrevLevel()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            UpdateLevelUI();
        }
    }
    private void SelectLevel()
    {
        selectedLevelIndex = currentLevelIndex;
        SaveSelectedLevel();
        UpdateSelectedLevelUI();
        UpdateLevelUI();
    }
    private void BuyUpgrade(int upgradeIndex)
    {
        CharacterLevel selectedLevel = levels[selectedLevelIndex];
        if (upgradeIndex < selectedLevel.upgrades.Length)
        {
            Upgrade upgrade = selectedLevel.upgrades[upgradeIndex];

            if (!upgrade.isPurchased && ZolotoManager.Instance.HasEnoughCrystals(upgrade.upgradeCost))
            {
                ZolotoManager.Instance.SubtractZoloto(upgrade.upgradeCost);
                upgrade.isPurchased = true;
                SaveUpgradeState(selectedLevelIndex, upgradeIndex);
                UpdateSelectedLevelUI();
                UpdateLevelUI();

                if (upgradeIndex < upgradeButtons.Count && upgradeIndex < upgradePriceTexts.Count)
                {
                    upgradeButtons[upgradeIndex].gameObject.SetActive(false);
                    upgradePriceTexts[upgradeIndex].gameObject.SetActive(false);
                }
            }
        }
    }
    private void SaveUpgradeState(int levelIndex, int upgradeIndex)
    {
        string key = $"Level_{levelIndex}_Upgrade_{upgradeIndex}";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }
    private void SaveSelectedLevel()
    {
        PlayerPrefs.SetInt("SelectedLevelIndex", selectedLevelIndex);
        PlayerPrefs.SetString("SelectedLevelSprite", levels[selectedLevelIndex].levelSprite.name);
        PlayerPrefs.Save();
    }
    private void LoadGameData()
    {
        if (PlayerPrefs.HasKey("SelectedLevelIndex"))
        {
            selectedLevelIndex = PlayerPrefs.GetInt("SelectedLevelIndex");
        }

        for (int i = 0; i < levels.Length; i++)
        {
            
            for (int j = 0; j < levels[i].upgrades.Length; j++)
            {
                string upgradeKey = $"Level_{i}_Upgrade_{j}";
                bool isPurchased = PlayerPrefs.HasKey(upgradeKey) && PlayerPrefs.GetInt(upgradeKey) == 1;

                levels[i].upgrades[j].isPurchased = isPurchased;
                if (j < upgradeButtons.Count && j < upgradePriceTexts.Count)
                {
                    upgradeButtons[j].gameObject.SetActive(!isPurchased); 
                    upgradePriceTexts[j].gameObject.SetActive(!isPurchased); 

                    if (!isPurchased)
                    {
                        upgradePriceTexts[j].text = $"{levels[i].upgrades[j].upgradeCost}";
                    }
                }
            }
          
        }
       
    }
}   

    [System.Serializable]
public class CharacterLevel
{
    public string levelName;
    public Upgrade[] upgrades;
    public Sprite levelSprite;
}

[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public int upgradeCost;
    public bool isPurchased;
}


