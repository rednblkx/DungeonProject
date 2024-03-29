﻿using UnityEngine;

//[RequireComponent(typeof(WeaponController))]
public class WeaponFuelCellHandler : MonoBehaviour
{
    public bool online;

    [Tooltip("List of GameObjects representing the fuel cells on the weapon")]
    public GameObject[] fuelCells;
    [Tooltip("Cell local position when used")]
    public Vector3 fuelCellUsedPosition;
    [Tooltip("Cell local position before use")]
    public Vector3 fuelCellUnusedPosition = new Vector3(0f, -0.1f, 0f);

    WeaponController m_Weapon;
    WeaponController_Photon m_Weapon_p;
    bool[] m_FuelCellsCooled;

    void Start()
    {
        if (online)
        {
            m_Weapon_p = GetComponent<WeaponController_Photon>();
            DebugUtility.HandleErrorIfNullGetComponent<WeaponController_Photon, WeaponFuelCellHandler>(m_Weapon_p, this, gameObject);

            m_FuelCellsCooled = new bool[fuelCells.Length];
            for (int i = 0; i < m_FuelCellsCooled.Length; i++)
            {
                m_FuelCellsCooled[i] = true;
            }
        }
        else
        {
            m_Weapon = GetComponent<WeaponController>();
            DebugUtility.HandleErrorIfNullGetComponent<WeaponController, WeaponFuelCellHandler>(m_Weapon, this, gameObject);

            m_FuelCellsCooled = new bool[fuelCells.Length];
            for (int i = 0; i < m_FuelCellsCooled.Length; i++)
            {
                m_FuelCellsCooled[i] = true;
            }
        }

    }

    void Update()
    {

        if (online)
        {
            for (int i = 0; i < fuelCells.Length; i++)
            {
                float length = fuelCells.Length;
                float lim1 = i / length;
                float lim2 = (i + 1) / length;

                float value = Mathf.InverseLerp(lim1, lim2, m_Weapon_p.currentAmmoRatio);
                value = Mathf.Clamp01(value);

                fuelCells[i].transform.localPosition = Vector3.Lerp(fuelCellUsedPosition, fuelCellUnusedPosition, value);
            }
        }
        else
        {
            for (int i = 0; i < fuelCells.Length; i++)
            {
                float length = fuelCells.Length;
                float lim1 = i / length;
                float lim2 = (i + 1) / length;

                float value = Mathf.InverseLerp(lim1, lim2, m_Weapon.currentAmmoRatio);
                value = Mathf.Clamp01(value);

                fuelCells[i].transform.localPosition = Vector3.Lerp(fuelCellUsedPosition, fuelCellUnusedPosition, value);
            }
        }

    }
}
