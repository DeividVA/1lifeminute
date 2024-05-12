using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodSO : ScriptableObject
{
    [SerializeField] public GameObject foodPrefab;
    [SerializeField] public int value;
}