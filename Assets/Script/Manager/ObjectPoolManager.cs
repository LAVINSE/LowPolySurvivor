using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject[] prefabs;

    private List<GameObject>[] pools;
    #endregion // 변수

    #region 함수
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i< pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(prefabs[index], this.transform);
            pools[index].Add(select);
        }

        return select;
    }
    #endregion // 함수
}
