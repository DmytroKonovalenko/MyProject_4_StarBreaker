using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AbsurdGooseContraption : MonoBehaviour
{
    #region Fields

    public Sprite[] honkTextures;
    public string[] honkCaptions;
    public string[] honkBlurbs;

    public Image gooseAvatar;
    public TextMeshProUGUI gooseHeader;
    public TextMeshProUGUI gooseSubtext;

    public Button squawkButton;
    public TextMeshProUGUI squawkButtonText;

    private int gooseCounter = 0;
    private float squawkDuration = 0.5f;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (PlayerPrefs.GetInt("GooseSighting", 0) == 1)
        {
            gameObject.SetActive(false);
        }

        InitiateGooseLore(false);
    }

    #endregion

    #region Public Methods

    public void LaunchGooseIntoOrbit()
    {
        gooseCounter++;
        if (gooseCounter >= honkTextures.Length)
        {
            gooseCounter = 0;
            PlayerPrefs.SetInt("GooseSighting", 1);
            PlayerPrefs.Save();
            SealTheSquawkGate();
            return;
        }

        SquawkManifestation();
    }

    public void UnleashTheGooseRealm()
    {
        gooseCounter = 0;
        InitiateGooseLore(false);
        gameObject.SetActive(true);

        transform.localPosition = new Vector3(0, Screen.height, 0);
        transform.DOLocalMoveY(0, squawkDuration).SetEase(Ease.OutBounce);
    }

    public void SealTheSquawkGate()
    {
        transform.DOLocalMoveY(Screen.height, squawkDuration).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }

    #endregion

    #region Private Methods

    private void SquawkManifestation()
    {
        Sequence squawkSequence = DOTween.Sequence();

        squawkSequence.Append(gooseAvatar.transform.DOLocalMoveY(-50, squawkDuration / 2).SetEase(Ease.InQuad));
        squawkSequence.Join(gooseHeader.DOFade(0, squawkDuration / 2));
        squawkSequence.Join(gooseSubtext.DOFade(0, squawkDuration / 2));

        squawkSequence.AppendCallback(() =>
        {
            InitiateGooseLore(true);
        });

        squawkSequence.Append(gooseAvatar.transform.DOLocalMoveY(0, squawkDuration / 2).SetEase(Ease.OutQuad));
        squawkSequence.Join(gooseHeader.DOFade(1, squawkDuration / 2));
        squawkSequence.Join(gooseSubtext.DOFade(1, squawkDuration / 2));
    }

    private void InitiateGooseLore(bool squawk)
    {
        gooseAvatar.sprite = honkTextures[gooseCounter];
        gooseHeader.text = honkCaptions[gooseCounter];
        gooseSubtext.text = honkBlurbs[gooseCounter];

        if (gooseCounter == 0)
        {
            squawkButtonText.text = "START NOW";
        }
        else if (gooseCounter == honkTextures.Length - 1)
        {
            squawkButtonText.text = "LET`S GO!";
        }
        else
        {
            squawkButtonText.text = "NEXT";
        }

        if (!squawk)
        {
            gooseAvatar.transform.localScale = Vector3.one;
            gooseHeader.color = new Color(gooseHeader.color.r, gooseHeader.color.g, gooseHeader.color.b, 1);
            gooseSubtext.color = new Color(gooseSubtext.color.r, gooseSubtext.color.g, gooseSubtext.color.b, 1);
        }
        else
        {
            gooseAvatar.transform.localPosition = new Vector3(0, 50, 0);
            gooseHeader.color = new Color(gooseHeader.color.r, gooseHeader.color.g, gooseHeader.color.b, 0);
            gooseSubtext.color = new Color(gooseSubtext.color.r, gooseSubtext.color.g, gooseSubtext.color.b, 0);

            Sequence gooseSequence = DOTween.Sequence();
            gooseSequence.Append(gooseAvatar.transform.DOLocalMoveY(0, squawkDuration / 2).SetEase(Ease.OutQuad));
            gooseSequence.Join(gooseHeader.DOFade(1, squawkDuration / 2));
            gooseSequence.Join(gooseSubtext.DOFade(1, squawkDuration / 2));
        }
    }

    #endregion
}
