using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Key : MonoBehaviour
{
    internal delegate void KeyPressed();
    internal event KeyPressed OnKeyPressed;

    internal delegate void KeyReleased();
    internal event KeyReleased OnKeyReleased;

    internal delegate void KeyInit();
    internal event KeyInit OnKeyInit;

    public KeyType KeyType
    {
        get { return keyData.KeyType; }
        set { keyData = new KeyData() { KeyType = value }; }
    }

    [SerializeField] private TextMeshProUGUI keyLabel;

    internal Animator animator;
    internal KeyData keyData;

    [SerializeField] private RectTransform _rectTransform;

    private Coroutine _updateCoroutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetKeyData(KeyData newKeyData)
    {
        keyData = newKeyData;
        Init();
        OnKeyInit?.Invoke();
    }

    internal virtual void Init()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Spawn");

        keyLabel.text = keyData.KeyName;
        _rectTransform.position = keyData.Position;
        _rectTransform.localScale = Vector2.one * keyData.Size;

        if (keyData.LifeTime != 0) StartCoroutine(LifeTimeCoroutine(keyData.LifeTime));
        _updateCoroutine = StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            var key = Keyboard.current.FindKeyOnCurrentKeyboardLayout(keyData.KeyName);

            if (key.wasPressedThisFrame)
            {
                OnKeyPressed?.Invoke();
            }
            else if (key.wasReleasedThisFrame)
            {
                OnKeyReleased?.Invoke();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator LifeTimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        FailedDestroyKey();
    }

    internal void DestroyKey()
    {
        StartCoroutine(DestroyKeyCoroutine(false));
    }

    internal void FailedDestroyKey()
    {
        StartCoroutine(DestroyKeyCoroutine(true));
    }

    private IEnumerator DestroyKeyCoroutine(bool isFailed)
    {
        StopCoroutine(_updateCoroutine);
        _updateCoroutine = null;

        if (isFailed) animator.SetTrigger("FailedDestroy");
        else animator.SetTrigger("Destroy");

        yield return new WaitForEndOfFrame();

        float duration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Debug.Log(duration);
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
