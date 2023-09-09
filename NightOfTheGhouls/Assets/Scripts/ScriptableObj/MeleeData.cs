using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Melee")]
public class MeleeData : ScriptableObject
{
    public string mName;

    [Header("Stats")]
    public float mDamage;
    public float mCritModifier;
    [Range(0.0f, 100.0f)] public float mCritChance;
    [Range(0.0f, 100.0f)] public float mAccuracy;
    public float mRange;
    public float mSwingRate;
}
