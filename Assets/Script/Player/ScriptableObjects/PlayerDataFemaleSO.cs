using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Female", menuName = "Scriptable Objects/PlayerDataSO/PlayerDataFemale")]
public class PlayerDataFemaleSO : PlayerDataSO
{
    public override void Stats()
    {
        base.Stats();
        Debug.Log(" Å×½ºÆ® 1");
    }
}
