using UnityEngine;
using DG.Tweening;

public class PopupAnimator : MonoBehaviour
{
    public enum AnimationDirection
    {
        FromBottom,
        FromTop,
        FromLeft,
        FromRight
    }

    public AnimationDirection openDirection = AnimationDirection.FromBottom;
    public AnimationDirection closeDirection = AnimationDirection.FromBottom;

    public float animationTime = 0.5f;

    public void OpenPopup()
    {
        gameObject.SetActive(true);

        Vector3 startPosition = transform.localPosition;

        switch (openDirection)
        {
            case AnimationDirection.FromBottom:
                startPosition.y = -Screen.height;
                break;
            case AnimationDirection.FromTop:
                startPosition.y = Screen.height;
                break;
            case AnimationDirection.FromLeft:
                startPosition.x = -Screen.width;
                break;
            case AnimationDirection.FromRight:
                startPosition.x = Screen.width;
                break;
        }

        transform.localPosition = startPosition;

        Vector3 endPosition = Vector3.zero;
        transform.DOLocalMove(endPosition, animationTime).SetEase(Ease.OutBack);
    }

    public void ClosePopup()
    {
        Vector3 endPosition = transform.localPosition;

        switch (closeDirection)
        {
            case AnimationDirection.FromBottom:
                endPosition.y = -Screen.height;
                break;
            case AnimationDirection.FromTop:
                endPosition.y = Screen.height;
                break;
            case AnimationDirection.FromLeft:
                endPosition.x = -Screen.width;
                break;
            case AnimationDirection.FromRight:
                endPosition.x = Screen.width;
                break;
        }

        transform.DOLocalMove(endPosition, animationTime).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
