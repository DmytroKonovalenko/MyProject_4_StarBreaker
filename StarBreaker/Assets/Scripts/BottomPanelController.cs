using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BottomPanelController : MonoBehaviour
{
    [SerializeField] private GameObject[] panels; // ����� ������� (3 ��.)
    [SerializeField] private Button[] buttons;    // ����� ������ (3 ��.)
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private float transitionDuration = 0.5f; // ��� �������

    private int currentPanelIndex = 0;

    private void Start()
    {
        // ���������� ������� ������� �����������
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Գ����� ����� ��� ������-�������
            buttons[i].onClick.AddListener(() => SwitchPanel(index));
        }

        // ����������� �������
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
            panels[i].transform.localScale = i == currentPanelIndex ? Vector3.one : Vector3.zero;
        }

        UpdateButtons();
    }

    private void SwitchPanel(int newIndex)
    {
        if (newIndex == currentPanelIndex) return; // ���� ��������� �� � ������ � ����� �� ������

        // ��������� ������� ������ � �������� ���������
        panels[currentPanelIndex].transform.DOScale(Vector3.zero, transitionDuration)
            .OnComplete(() =>
            {
                panels[currentPanelIndex].SetActive(false);

                // �������� ���� ������ � ������ ����� "��������"
                panels[newIndex].SetActive(true);
                panels[newIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1);
                panels[newIndex].transform.DOScale(Vector3.one, transitionDuration)
                    .SetEase(Ease.OutBack); // ������ ������ ������� ��� ���������
            });

        // ��������� ������� ������
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
