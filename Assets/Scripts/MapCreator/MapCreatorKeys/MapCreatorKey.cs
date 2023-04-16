using System;
using UnityEngine;
using TMPro;

public class MapCreatorKey : MonoBehaviour
{
    [SerializeField] private RectTransform settingsMenu;
    [SerializeField] private TextMeshProUGUI currentKeyLabel;
    [SerializeField] private TMP_InputField keyInputField;

    private RectTransform _rectTransform;
    public KeyData KeyData;

    private MapCreatorDataSaver _dataSaver;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(MapCreatorDataSaver dataSaver, float spawnTime)
    {
        _dataSaver = dataSaver;

        KeyData = new KeyData();
        KeyData.KeyName = "A";
        KeyData.Size = 1;
        KeyData.SpawnTime = spawnTime;
        KeyData.LifeTime = 5f;
        KeyData.HoldDuration = 5f;
        KeyData.RequiredClicks = 3;
        KeyData.Id = _dataSaver.AddKeyData(this);

        _dataSaver.UpdateKeyData(KeyData);
    }

    public void Scale(Single value)
    {
        settingsMenu.SetParent(_rectTransform.parent);

        if (value == 0) value = 0.5f;
        KeyData.Size = value;
        _rectTransform.localScale = Vector2.one * (KeyData.Size + 0.05f);

        settingsMenu.SetParent(_rectTransform);
        UpdateData();
    }

    public void SetKey(String value)
    {
        if (value == "" || !KeyboardAlphabet.Alphabet.Contains(value.ToUpper()))
        {
            keyInputField.text = KeyData.KeyName;
            return;
        }

        currentKeyLabel.text = KeyData.KeyName = value.ToUpper();
        UpdateData();
    }

    public void SetLifeTime(String value)
    {
        KeyData.LifeTime = Convert.ToSingle(value);
        UpdateData();
    }

    public void UpdatePosition()
    {
        KeyData.Position = _rectTransform.position;
        UpdateData();
    }

    internal void UpdateData()
    {
        _dataSaver.UpdateKeyData(KeyData);
    }
}
