using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MapDataLoader : MonoBehaviour
{
    [SerializeField] private KeySpawner keySpawner;
    [SerializeField] private string mapFilePath;

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        string data = File.ReadAllText(Application.persistentDataPath + mapFilePath);
        MapData mapData = JsonUtility.FromJson<MapData>(data);
        List<KeyData> keyList = mapData.KeyDataList;

        keySpawner.CreatePool(keyList.Count);
        SpawnKeys(keyList);
    }

    private void SpawnKeys(List<KeyData> keyList)
    {
        keySpawner.EnableKeys(keyList);
    }
}
