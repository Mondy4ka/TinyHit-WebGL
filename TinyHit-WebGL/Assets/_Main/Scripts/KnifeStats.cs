using System.Collections.Generic;
using UnityEngine;

public class KnifeStats
{
    public Sprite Sprite {  get; private set; }
    public float Damage { get; private set; }
    public List<Effect> Effects { get; private set; } = new();
}
