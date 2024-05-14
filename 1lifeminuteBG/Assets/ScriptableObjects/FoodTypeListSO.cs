using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodTypeListSO : ScriptableObject
{
    [SerializeField] public List<FoodTypeSO> foods;
}
