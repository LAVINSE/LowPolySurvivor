using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolList<T>
{
    public List<T> activeList = new List<T>(); // Ȱ��ȭ ��ü
    public Queue<T> inActiveQueue = new Queue<T>(); // ��Ȱ��ȭ ��ü
}

public class ObjectPoolManager : MonoBehaviour
{
    #region ����
    private Dictionary<System.Type, PoolList<object>> poolListDict = new Dictionary<System.Type, PoolList<object>>();
    #endregion // ����


    #region �Լ�
    /** ��ü�� Ȱ��ȭ �Ѵ� */
    public object SpawnObj<T>(System.Func<object> Create)
    {
        var PoolList = poolListDict.GetValueOrDefault(typeof(T)) ?? new PoolList<object>();
        // Queue�� ��Ȱ�� ��ü�� �ϳ��̻� ������ Dequeue ������ ���(��Ȱ��ȭ ��ü ��������) , Queue�� ������ Creator���� ����
        // Creator() ��ȣ �κ��� �Լ��� ȣ���Ѵٶ�� ���̴�
        var Obj = (PoolList.inActiveQueue.Count >= 1) ? PoolList.inActiveQueue.Dequeue() : Create();

        // �ߺ� ��ü�� �ƴ� ���
        if (!PoolList.activeList.Contains(Obj))
        {
            PoolList.activeList.Add(Obj);
        }

        poolListDict.TryAdd(typeof(T), PoolList);

        return Obj;
    }

    /** ��ü�� ��Ȱ��ȭ �Ѵ� */
    public void DeSpawnObj<T>(object Objs, System.Action<object> Callback)
    {
        // ��ü Ǯ�� ���� �� ���
        // Key�� �ش��ϴ� Value ���� ������ true ��ȯ �� ���� out���� ���޵� PoolList�� ���� >> out�� ����
        if (poolListDict.TryGetValue(typeof(T), out PoolList<object> PoolList))
        {
            PoolList.activeList.Remove(Objs); // List���� ����
            PoolList.inActiveQueue.Enqueue(Objs); // Queue�� �߰�

            Callback?.Invoke(Objs);
        }
    }

    #endregion // �Լ�
}
