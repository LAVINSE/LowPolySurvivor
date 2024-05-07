using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region ������Ƽ
    public GameObject UIRoot { get; private set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    protected virtual void Awake()
    {
        Application.targetFrameRate = 60;

        var RootObjs = this.gameObject.scene.GetRootGameObjects();

        for (int i = 0; i < RootObjs.Length; i++)
        {
            this.UIRoot = this.UIRoot ??
                RootObjs[i].transform.Find("Canvas/UIs/UIRoot")?.gameObject;
        }
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    protected virtual void Update()
    {
        OptionShow();
    }

    /** �ɼ� �˾��� �����ش� */
    public void OptionShow(bool IsClick = false)
    {
        // Esc Ű�� ������ ���
        if (Input.GetKeyDown(KeyCode.Escape) || IsClick == true)
        {
            var Option = UIRoot.GetComponentInChildren<OptionPopupUI>();

            // �ɼ� �˾��� ���� �� ���
            if (Option != null)
            {
                Option.PopupClose();
            }
            else
            {
                Option = OptionPopupUI.CreateOptionPopup(UIRoot);

                Option.PopupShow();
            }
        }
    }
    #endregion // �Լ�
}
