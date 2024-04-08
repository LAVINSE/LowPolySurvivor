using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSelect : MonoBehaviour
{
    public eEquipType equipType;
    public Image image;

    public void ChangeEquip(eEquipType type, Image image)
    {
        equipType = type;
        this.image.sprite = image.sprite;
    }
}
