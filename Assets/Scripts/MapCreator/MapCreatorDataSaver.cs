using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MapCreatorDataSaver : MonoBehaviour
{
    public List<KeyData> KeyDataList = new List<KeyData>();
    public List<MapCreatorKey> KeyList = new List<MapCreatorKey>();

    public int AddKeyData(MapCreatorKey key)
    {
        int lastId = -1;

        if (KeyDataList.Count > 0)
        {
            lastId = KeyDataList[KeyDataList.Count - 1].Id;
        }

        KeyDataList.Add(new KeyData());
        KeyList.Add(key);

        return lastId + 1;
    }

    public void UpdateKeyData(KeyData newData)
    {
        KeyData keyData = KeyDataList.Find(key => key.Id == newData.Id);
        int index = KeyDataList.IndexOf(keyData);
        KeyDataList[index] = newData;
    }

    public void RemoveKeyData(int id)
    {
        KeyDataList.Remove(KeyDataList.Find(key => key.Id == id));
        KeyList.Remove(KeyList.Find(obj => obj.KeyData.Id == id));
    }

    public void SaveData()
    {
        MapData mapData = new MapData();
        mapData.KeyDataList = KeyDataList;

        string json = JsonUtility.ToJson(mapData);
        File.WriteAllText(Application.persistentDataPath + "/map_data.json", json);
        Debug.Log(Application.persistentDataPath);
    }
}
