using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiveWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> unLockList = new List<GameObject>();
    [SerializeField] private List<GameObject> lockList = new List<GameObject>();

    public eEquipType[] eEquipType;

    private void Awake()
    {
        /*
        eEquipType = (eEquipType[])Enum.GetValues(typeof(eEquipType));

        if (!PlayerPrefs.HasKey("MyData"))
        {
            PlayerPrefs.SetInt("MyData", 1);

            foreach (eEquipType type in eEquipType)
            {
                PlayerPrefs.GetInt(type.ToString(), 0);
            }
        }
        */
    }

    private void Unlock()
    {
        /*
        for(int i = 0; i < unLockList.Count; i++)
        {
            string typeName = eEquipType[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(typeName) == 1;
            unLockList[i].SetActive(isUnlock);
            lockList[i].SetActive(!isUnlock);
        }
        */
    }
}
