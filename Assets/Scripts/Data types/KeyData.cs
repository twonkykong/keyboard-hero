using System;
using UnityEngine;

[Serializable]
public struct KeyData
{
    public KeyType KeyType;
    public Vector2 Position;
    public string KeyName;
    public int Id;
    public float Size;
    public float SpawnTime;
    public float LifeTime;
    public float HoldDuration;
    public int RequiredClicks;
}
