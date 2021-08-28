using System;
using UnityEngine;

[Serializable]
public class ObjectPoolItem
{
    public string tag;
    public int amountToPool;
    public GameObject objectToPool;
    public int expandAmount;
}