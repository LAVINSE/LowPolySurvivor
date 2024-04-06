using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프리팹 배열이랑 똑같은 번호로 맞추기
public enum BulletType
{
    SubmachineGunBullet = 0,
    AssaultGunBullet,
    ShotGunBullet,
    MachineGunBullet,
}

public enum EnemyType
{

}

public class ObjectPoolManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject[] bulletPrefabArray;
    [SerializeField] private GameObject[] enemyPrefabArray;

    private List<GameObject>[] bulletPoolList;
    private List<GameObject>[] enemyPoolList;
    #endregion // 변수

    #region 함수
    private void Awake()
    {
        bulletPoolList = new List<GameObject>[bulletPrefabArray.Length];

        for(int i = 0; i < bulletPoolList.Length; i++)
        {
            bulletPoolList[i] = new List<GameObject>();
        }


        enemyPoolList = new List<GameObject>[enemyPrefabArray.Length];

        for(int i = 0; i < enemyPoolList.Length; i++)
        {
            enemyPoolList[i] = new List<GameObject>();
        }
    }

    public GameObject GetBullet(int index, Vector3 spawnPos)
    {
        GameObject select = null;

        foreach(GameObject item in bulletPoolList[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = spawnPos;
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(bulletPrefabArray[index], spawnPos, Quaternion.identity);
            select.transform.SetParent(this.transform);
            bulletPoolList[index].Add(select);
        }

        return select;
    }

    public GameObject GetEnemy(int index, Vector3 spawnPos)
    {
        GameObject select = null;

        foreach (GameObject item in enemyPoolList[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = spawnPos;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(enemyPrefabArray[index], spawnPos, Quaternion.identity);
            select.transform.SetParent(this.transform);
            enemyPoolList[index].Add(select);
        }

        return select;
    }
    #endregion // 함수
}
