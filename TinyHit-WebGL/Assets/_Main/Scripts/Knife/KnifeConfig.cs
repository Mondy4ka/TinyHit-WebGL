using UnityEngine;

[CreateAssetMenu(fileName = "Knife Config", menuName = "Knife Config")]
public class KnifeConfig : ScriptableObject
{
    public Sprite Sprite;
    public int Id;
    public string Name;
    public int Price;
    public float Damage;
}