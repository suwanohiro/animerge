using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public struct RankingData
{
    public string Name;
    public int Score;
}

[Serializable]
public struct RankingArray
{
    public RankingData[] Ranking;
}