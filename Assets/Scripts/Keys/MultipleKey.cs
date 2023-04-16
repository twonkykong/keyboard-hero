using UnityEngine;
using TMPro;

public class MultipleKey : Key
{
    [SerializeField] private TextMeshProUGUI clicksLabel;
    private int _clicksCount = 0;

    private void Start()
    {
        OnKeyPressed += CountClicks;
    }

    internal override void Init()
    {
        base.Init();
        clicksLabel.text = keyData.RequiredClicks.ToString();
    }

    private void CountClicks()
    {
        _clicksCount += 1;
        clicksLabel.text = _clicksCount.ToString();

        if (_clicksCount < keyData.RequiredClicks)
        {
            animator.SetTrigger("Click" + Random.Range(1, 3));
        }
        else
        {
            DestroyKey();
        }
    }

    private void OnDestroy()
    {
        OnKeyPressed -= CountClicks;
    }
}
