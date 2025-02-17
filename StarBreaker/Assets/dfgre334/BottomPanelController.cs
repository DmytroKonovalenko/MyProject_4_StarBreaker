using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BottomPanelController : MonoBehaviour
{
    [SerializeField] private GameObject[] panels; // Масив панелей (3 шт.)
    [SerializeField] private Button[] buttons;    // Масив кнопок (3 шт.)
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private float transitionDuration = 0.5f; // Час анімації

    private int currentPanelIndex = 0;

    private void Start()
    {
        // Призначаємо кнопкам функцію перемикання
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Фіксуємо змінну для лямбда-функції
            buttons[i].onClick.AddListener(() => SwitchPanel(index));
        }

        // Ініціалізація панелей
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
            panels[i].transform.localScale = i == currentPanelIndex ? Vector3.one : Vector3.zero;
        }

        UpdateButtons();
    }

    private void SwitchPanel(int newIndex)
    {
        if (newIndex == currentPanelIndex) return; // Якщо натиснута та ж панель — нічого не робимо

        // Закриваємо поточну панель з анімацією зменшення
        panels[currentPanelIndex].transform.DOScale(Vector3.zero, transitionDuration)
            .OnComplete(() =>
            {
                panels[currentPanelIndex].SetActive(false);

                // Активуємо нову панель і робимо ефект "відкриття"
                panels[newIndex].SetActive(true);
                panels[newIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1);
                panels[newIndex].transform.DOScale(Vector3.one, transitionDuration)
                    .SetEase(Ease.OutBack); // Додаємо легкий підстриб при відкриванні
            });

        // Оновлюємо активну кнопку
        currentPanelIndex = newIndex;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = (i == currentPanelIndex) ? activeColor : defaultColor;
        }
    }
}
