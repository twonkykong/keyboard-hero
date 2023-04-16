using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private int poolCount;
    [SerializeField] private Transform keyParent;
    [SerializeField] private GameObject defaultKeyPrefab, holdKeyPrefab, multipleKeyPrefab;

    [Header("")]
    [SerializeField] private List<GameObject> keysPool = new List<GameObject>();

    private void SpawnPoolKey(GameObject prefab, KeyType keyType)
    {
        GameObject newKey = Instantiate(prefab, keyParent);
        newKey.GetComponent<Key>().KeyType = keyType;
        newKey.transform.SetParent(keyParent);
        keysPool.Add(newKey);
    }

    public void CreatePool(int keyAmount)
    {
        if (keyAmount < poolCount) poolCount = keyAmount;

        for (int i = 0; i < poolCount/3; i++)
        {
            SpawnPoolKey(defaultKeyPrefab, KeyType.Default);
            SpawnPoolKey(holdKeyPrefab, KeyType.Hold);
            SpawnPoolKey(multipleKeyPrefab, KeyType.Multiple);
        }
    }

    public void EnableKeys(List<KeyData> keyList)
    {
        foreach (KeyData key in keyList)
        {
            StartCoroutine(EnableKey(key));
        }
    }

    private IEnumerator EnableKey(KeyData keyData)
    {
        yield return new WaitForSeconds(keyData.SpawnTime);
        List<GameObject> disabledKeysList = keysPool.FindAll(obj => obj.activeInHierarchy == false);

        GameObject keyObj;
        try { keyObj = disabledKeysList.First(obj => obj.GetComponent<Key>().KeyType == keyData.KeyType); }
        catch
        {
            GameObject referenceKeyObj = keysPool.First(obj => obj.GetComponent<Key>().KeyType == keyData.KeyType);
            keyObj = Instantiate(referenceKeyObj, keyParent);
            keysPool.Add(keyObj);
        }
        
        Key key = keyObj.GetComponent<Key>();
        key.SetKeyData(keyData);
    }
}
