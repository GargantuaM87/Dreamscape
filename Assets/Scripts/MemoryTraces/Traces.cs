using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Traces", menuName = "Scriptable Objects/Traces")]
public abstract class Traces : ScriptableObject
{
    [SerializeField] private string traceName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private Rarity rarity;

    public abstract void Execute();


    public string Name { get { return traceName; } }
    public string Desc { get { return description; } }
    public Rarity tRarity { get { return rarity; } }
}
