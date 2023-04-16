using UnityEngine;

public class MapCreatorKeySpawner : MonoBehaviour
{
    [SerializeField] private MapCreatorDataSaver dataSaver;
    [SerializeField] private MapCreatorMusicPreview musicPreview;
    [SerializeField] private MapCreatorKeyMover keyMover;
    [SerializeField] private Transform canvas;

    [Header("")]
    [SerializeField] private GameObject regularKeyPrefab;
    [SerializeField] private GameObject holdKeyPrefab;
    [SerializeField] private GameObject multipleKeyPrefab;

    [Header("")]
    [SerializeField] private Transform regularKeySpawner;
    [SerializeField] private Transform holdKeySpawner;
    [SerializeField] private Transform multipleKeySpawner;

    private void SpawnKey(GameObject prefab, Vector2 position)
    {
        Transform newKey = Instantiate(prefab, canvas).transform;
        newKey.position = position;
        newKey.gameObject.GetComponent<MapCreatorKey>().Init(dataSaver, musicPreview.CurrentTime);
        keyMover.StartMoving(newKey);
    }

    public void SpawnRegularKey()
    {
        SpawnKey(regularKeyPrefab, regularKeySpawner.position);
    }

    public void SpawnHoldKey()
    {
        SpawnKey(holdKeyPrefab, holdKeySpawner.position);
    }

    public void SpawnMultipleKey()
    {
        SpawnKey(multipleKeyPrefab, multipleKeySpawner.position);
    }
}
