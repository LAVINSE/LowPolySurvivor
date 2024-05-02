using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    #region ����
    private Tween ShowDoTween = null;
    private Tween CloseDoTween = null;
    #endregion // ����

    #region ������Ƽ
    protected GameObject Option_Background_Img { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {

    }

    /** �ʱ�ȭ >> ���� �Ǿ��� ��� */
    private void OnDestroy()
    {
        ResetDoTween();
    }

    /** DoTween�� �����Ѵ� */
    public void ResetDoTween()
    {
        // DOTween ����
        ShowDoTween?.Kill();
        CloseDoTween?.Kill();
    }

    /** �˾��� ����Ѵ� */
    public void PopupShow()
    {
        ResetDoTween();

        // ���� ���·� ���
        Option_Background_Img.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        // ũ�� ����
        ShowDoTween = Option_Background_Img.transform.DOScale(Vector3.one, 0.15f).SetAutoKill();
    }

    /** �˾��� �ݴ´� */
    public void PopupClose()
    {
        ResetDoTween();
        CloseDoTween = Option_Background_Img.transform.DOScale
        (new Vector3(0.01f, 0.01f, 0.01f), 0.15f).SetAutoKill();

        // �˾��� �ݰ� OncompleteClose ����
        CloseDoTween.onComplete = OnCompleteClose;
    }

    /** �ݱ� DoTween�� �Ϸ� �Ǿ��� ���*/
    private void OnCompleteClose()
    {
        Destroy(this.gameObject);
    }
    #endregion // �Լ�
}
