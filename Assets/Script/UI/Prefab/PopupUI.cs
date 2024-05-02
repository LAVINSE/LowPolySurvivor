using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    #region 변수
    private Tween ShowDoTween = null;
    private Tween CloseDoTween = null;
    #endregion // 변수

    #region 프로퍼티
    protected GameObject Option_Background_Img { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {

    }

    /** 초기화 >> 제거 되었을 경우 */
    private void OnDestroy()
    {
        ResetDoTween();
    }

    /** DoTween을 리셋한다 */
    public void ResetDoTween()
    {
        // DOTween 중지
        ShowDoTween?.Kill();
        CloseDoTween?.Kill();
    }

    /** 팝업을 출력한다 */
    public void PopupShow()
    {
        ResetDoTween();

        // 작은 상태로 대기
        Option_Background_Img.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        // 크기 증가
        ShowDoTween = Option_Background_Img.transform.DOScale(Vector3.one, 0.15f).SetAutoKill();
    }

    /** 팝업을 닫는다 */
    public void PopupClose()
    {
        ResetDoTween();
        CloseDoTween = Option_Background_Img.transform.DOScale
        (new Vector3(0.01f, 0.01f, 0.01f), 0.15f).SetAutoKill();

        // 팝업을 닫고 OncompleteClose 실행
        CloseDoTween.onComplete = OnCompleteClose;
    }

    /** 닫기 DoTween이 완료 되었을 경우*/
    private void OnCompleteClose()
    {
        Destroy(this.gameObject);
    }
    #endregion // 함수
}
