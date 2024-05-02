using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 프로퍼티
    public GameObject UiRoot { get; private set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        var RootObjs = this.gameObject.scene.GetRootGameObjects();

        for (int i = 0; i < RootObjs.Length; i++)
        {
            this.UiRoot = this.UiRoot ??
                RootObjs[i].transform.Find("Canvas/UiRoot")?.gameObject;
        }
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        OptionShow();
    }

    /** 옵션 팝업을 보여준다 */
    public void OptionShow(bool IsClick = false)
    {
        // Esc 키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Escape) || IsClick == true)
        {
            //var Option = UiRoot.GetComponentInChildren<OptionPopup>();

            /*
            // 옵션 팝업이 존재 할 경우
            if (Option != null)
            {
                Option.PopupClose();
            }
            else
            {
                //Option = OptionPopup.CreateOptionPopup("옵션", UiRoot);
            }
            */
        }
    }
    #endregion // 함수
}
