using System;
using UnityEngine;
using TMPro;

public class MapCreatorMultipleKey : MapCreatorKey
{
    [SerializeField] private TMP_InputField clicksInputField;
    [SerializeField] private TextMeshProUGUI clicksAmount;

    private void OnEnable()
    {
        KeyData.KeyType = KeyType.Multiple;
    }

    public void SetRequiredClicksAmount(String value)
    {
        if (value == "0")
        {
            clicksInputField.text = KeyData.RequiredClicks.ToString();
            return;
        }

        KeyData.RequiredClicks = Convert.ToInt32(value);
        clicksAmount.text = value;
        UpdateData();
    }
}
