using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunConfigs", menuName = MenuPath, order = MenuOrder)]
public class GunConfigs : ScriptableObject, IGunConfigs
{
    private const string MenuPath = "Configs/GunConfigs";
    private const int MenuOrder = int.MinValue + 121;

    [SerializeField] private float _fireRate = 0.5f;

    public float FireRate => _fireRate;
}
