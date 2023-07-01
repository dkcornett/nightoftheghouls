using UnityEngine;

[CreateAssetMenu(fileName="Weapon", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{
    public string mName;

    [Header("Stats")]
    public int mMaxAmmo;
    public int mMagSize;
    public float mDamage;
    public float mCritModifier;
    [Range(0.0f, 100.0f)] public float mCritChance;
    [Range(0.0f, 100.0f)] public float mAccuracy;
    public float mRange;
    public float mFireRate;
    public float mReloadTime;
}