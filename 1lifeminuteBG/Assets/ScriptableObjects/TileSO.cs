using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileSO : ScriptableObject
{
    [SerializeField] public GameObject tilePrefab;
    [SerializeField] public bool startingTile;
    [SerializeField] private bool platformTile;
}