using UnityEngine;

[CreateAssetMenu(fileName="Weapon", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{
    public string mName;

    [Header("Stats")]
    public int mMaxAmmo;
    public int mMagSize;
    public float mDamage;
    public float mRange;
    public float mFireRate;
    public float mReloadTime;
}