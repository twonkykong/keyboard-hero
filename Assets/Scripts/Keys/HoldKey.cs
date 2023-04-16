using System.Collections;
using UnityEngine;
using DG.Tweening;

public class HoldKey : Key
{
    [SerializeField] private RectTransform holdBar;
    private Tween _holdBarScalingTween;

    private void Start()
    {
        OnKeyPressed += StartHoldCheck;
        OnKeyReleased += CancelHoldCheck;
    }

    private void StartHoldCheck()
    {
        StartCoroutine(HoldCoroutine());
    }

    private void CancelHoldCheck()
    {
        StopAllCoroutines();
        _holdBarScalingTween.Kill();

        FailedDestroyKey();
    }

    private IEnumerator HoldCoroutine()
    {
        animator.SetBool("isHolding", true);
        _holdBarScalingTween = holdBar.DOScaleX(1, keyData.HoldDuration).SetEase(Ease.Linear);

        yield return new WaitForSeconds(keyData.HoldDuration);

        animator.SetBool("isHolding", false);
        holdBar.localScale = new Vector2(0, 1);

        DestroyKey();
    }

    private void OnDestroy()
    {
        OnKeyPressed -= StartHoldCheck;
        OnKeyReleased -= CancelHoldCheck;
    }
}
