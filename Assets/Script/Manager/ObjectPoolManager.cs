using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �迭�̶� �Ȱ��� ��ȣ�� ���߱�
public enum PoolBulletType
{
    SubmachineGunBullet = 0,
    AssaultGunBullet,
    ShotGunBullet,
    MachineGunBullet,
    GrenadeBullet,
    RockBullet,
}

public enum PoolEnemyType
{
    Beholder,
    ChestMonster,
    Slime,
    Turtle,
}

public enum PoolBossType
{
    NightMareRedDragon
}

public enum PoolItemType
{
    Exp,
}

public class ObjectPoolManager : MonoBehaviour
{
    #region ����
    [SerializeField] private GameObject[] bulletPrefabArray;
    [SerializeField] private GameObject[] enemyPrefabArray;
    [SerializeField] private GameObject[] bossPrefabArray;
    [SerializeField] private GameObject[] itemPrefabArray;

    private List<GameObject>[] bulletPoolList;
    private List<GameObject>[] enemyPoolList;
    private List<GameObject>[] bossPoolList;
    private List<GameObject>[] itemPoolList;
    #endregion // ����

    #region �Լ�
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

        itemPoolList = new List<GameObject>[itemPrefabArray.Length];

        for(int i = 0; i < itemPoolList.Length; i++)
        {
            itemPoolList[i] = new List<GameObject>();
        }

        bossPoolList = new List<GameObject>[bossPrefabArray.Length];

        for(int i = 0; i < bossPoolList.Length; i++)
        {
            bossPoolList[i] = new List<GameObject>();
        }
    }

    /** �Ѿ� Ǯ�� */
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

    /** ���� Ǯ�� */
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

    /** ���� Ǯ�� */
    public GameObject GetBoss(int index, Vector3 spawnPos)
    {
        GameObject select = null;

        foreach (GameObject item in bossPoolList[index])
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
            select = Instantiate(bossPrefabArray[index], spawnPos, Quaternion.identity);
            select.transform.SetParent(this.transform);
            bossPoolList[index].Add(select);
        }

        return select;
    }

    /** ������ Ǯ�� */
    public GameObject GetItem(int index, Vector3 spawnPos)
    {
        GameObject select = null;

        foreach (GameObject item in itemPoolList[index])
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
            select = Instantiate(itemPrefabArray[index], spawnPos, Quaternion.identity);
            select.transform.SetParent(this.transform);
            itemPoolList[index].Add(select);
        }

        return select;
    }
    #endregion // �Լ�
}
