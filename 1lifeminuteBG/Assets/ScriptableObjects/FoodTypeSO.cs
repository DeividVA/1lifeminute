using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodTypeSO : ScriptableObject
{
    //[SerializeField] public int index;
    [SerializeField] public string food;
    [SerializeField] public GameObject foodPrefab;
    [SerializeField] public int healthyPoints;
    [SerializeField] public Color color;
}