using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlantSO : ScriptableObject
{
    public PlantStage[] Stages;
}

[Serializable]
public struct PlantStage
{
    public float TimeToNextStage;
    public Sprite SpriteStage;
}