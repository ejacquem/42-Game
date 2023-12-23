using UnityEngine;

[CreateAssetMenu(fileName = "BonusData", menuName = "ScriptableObjects/BonusData", order = 1)]
public class BonusData : ScriptableObject
{
    public GameObject[] bonusPrefabs; // Array of your bonus prefabs
}