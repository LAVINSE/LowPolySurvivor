using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectEquipUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<Button> selectButtonList = new List<Button>();
    [SerializeField] private List<GameObject> selectScrollList = new List<GameObject>();
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        for (int i = 0; i < selectButtonList.Count; i++)
        {
            int count = i;
            selectButtonList[i].onClick.AddListener(() => MoveScroll(selectScrollList[count]));
        }

        for (int i = 0; i < selectScrollList.Count; i++)
        {
            int index_i = i;
            Button[] buttonArray = selectScrollList[i].GetComponentsInChildren<Button>(true);
            EquipSelect[] equipselect = selectScrollList[i].GetComponentsInChildren<EquipSelect>(true);

            for(int j = 0; j < buttonArray.Length; j++)
            {
                int index_j = j;
                buttonArray[j].onClick.AddListener(() =>
                selectButtonList[index_i].GetComponent<EquipSelect>().
                ChangeEquip(equipselect[index_j].equipType, equipselect[index_j].image));
            }
        }
    }

    /** 장비 선택창을 움직인다 */
    public void MoveScroll(GameObject scroll)
    {
        bool setActive = !scroll.activeSelf;

        if (setActive)
        {
            float targetPosition = 310f;
            scroll.SetActive(setActive);
            scroll.transform.DOLocalMoveX(targetPosition, 0.6f).SetEase(Ease.Unset);
        }
        else
        {
            float targetPosition = 1110f;
            
            scroll.transform.DOLocalMoveX(targetPosition, 0.6f).SetEase(Ease.Unset).OnComplete(() =>
            {
                scroll.SetActive(setActive);
            });
        }
    }
    #endregion // 함수

    #region 코루틴
    #endregion // 코루틴
}
