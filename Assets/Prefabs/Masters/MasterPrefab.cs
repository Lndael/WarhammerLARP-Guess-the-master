using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MasterData", menuName = "ScriptableObjects/MasterPrefab", order = 51)]
public class MasterPrefab : ScriptableObject
{
    public Sprite MainPhoto;
    public Sprite OldPhoto;
    public string Name;
}
