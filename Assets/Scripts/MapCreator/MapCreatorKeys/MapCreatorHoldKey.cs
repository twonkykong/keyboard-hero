using System;
using UnityEngine;
using TMPro;

public class MapCreatorHoldKey : MapCreatorKey
{
    [SerializeField] private TMP_InputField durationInputField;

    private void OnEnable()
    {
        KeyData.KeyType = KeyType.Hold;
    }

    public void SetHoldDuration(String value)
    {
        if (value == "0")
        {
            durationInputField.text = KeyData.HoldDuration.ToString();
            return;
        }

        KeyData.HoldDuration = Convert.ToSingle(value);
        UpdateData();
    }
}
