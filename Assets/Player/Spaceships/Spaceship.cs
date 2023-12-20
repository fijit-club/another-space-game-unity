using System;
using UnityEngine;

[Serializable]
public class Spaceship
{
    public string name;
    public string id;
    public Sprite sprite;
    public int coinsRequired;
    public bool purchased;
    public Ability ability;
}