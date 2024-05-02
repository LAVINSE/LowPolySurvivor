using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region ������Ƽ
    public GameObject UiRoot { get; private set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        var RootObjs = this.gameObject.scene.GetRootGameObjects();

        for (int i = 0; i < RootObjs.Length; i++)
        {
            this.UiRoot = this.UiRoot ??
                RootObjs[i].transform.Find("Canvas/UiRoot")?.gameObject;
        }
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        OptionShow();
    }

    /** �ɼ� �˾��� �����ش� */
    public void OptionShow(bool IsClick = false)
    {
        // Esc Ű�� ������ ���
        if (Input.GetKeyDown(KeyCode.Escape) || IsClick == true)
        {
            //var Option = UiRoot.GetComponentInChildren<OptionPopup>();

            /*
            // �ɼ� �˾��� ���� �� ���
            if (Option != null)
            {
                Option.PopupClose();
            }
            else
            {
                //Option = OptionPopup.CreateOptionPopup("�ɼ�", UiRoot);
            }
            */
        }
    }
    #endregion // �Լ�
}
