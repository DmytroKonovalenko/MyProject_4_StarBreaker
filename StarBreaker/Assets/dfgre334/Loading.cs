using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    [SerializeField] private float activeTime = 2.5f;  
    [SerializeField] private float fadeDuration = 0.5f; 
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        Invoke(nameof(CloseAndDestroy), activeTime);
    }

    private void CloseAndDestroy()
    {

        canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
