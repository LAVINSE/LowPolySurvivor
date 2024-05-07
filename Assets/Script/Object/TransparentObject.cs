using System.Collections;
using System.Linq;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    #region 변수
    // 오브젝트 렌더러 저장
    private MeshRenderer[] meshRenderers;

    // 변화하는 속도 및 초기화 지연속도를 나타내는 변수
    private WaitForSeconds delay = new WaitForSeconds(0.001f);
    private WaitForSeconds resetDelay = new WaitForSeconds(0.005f);

    // 투명도와 리셋을 위한 값
    private const float MESHRENDERER_ALPHA = 0.25f;
    private const float MAX_TIMER = 0.5f;

    // 리셋중인지 확인하는 변수
    private bool isReseting = false;

    // 타이머 변수
    private float timer = 0f;

    // 코루틴 변수
    private Coroutine timeCheckCoroutine;
    private Coroutine resetCoroutine;
    private Coroutine becomeTransparentCoroutine;
    #endregion // 변수

    #region 프로퍼티
    public bool IsTransparent { get; private set; } = false; // 오브젝트가 투명한지 확인하는 변수
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        meshRenderers = GetComponents<MeshRenderer>();
    }

    /** 오브젝트를 투명하게 만든다 */
    public void BecomeTransparent()
    {
        // 이미 오브젝트가 투명할 경우
        if (IsTransparent)
        {
            // 타이머 초기화, 함수 종료
            timer = 0f;
            return;
        }

        // 코루틴이 존재하고, 리셋중일 경우
        if (resetCoroutine != null && isReseting)
        {
            // 투명도 변경
            isReseting = false;
            IsTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        // 투명도를 변경하고 코루틴을 실행
        SetMaterialTransparent();
        IsTransparent = true;
        becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCo());
    }

    /** 초기 투명도 상태로 리셋한다 */
    public void ResetOriginalTransparent()
    {
        // 머터리얼의 렌더링 모드를 불투명으로 변경한다
        SetMaterialOpaque();

        // 초기 투명도 상태로 리셋하는 코루틴을 실행한다
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCo());
    }

    /** 타이머를 체크한다 */
    public void CheckTimer()
    {
        // 타이머 체크 코루틴이 존재할 경우
        if (timeCheckCoroutine != null)
        {
            // 진행중이던 타이머 체크 코루틴을 중지한다
            StopCoroutine(timeCheckCoroutine);
        }

        // 타이머 체크 코루틴을 실행한다
        timeCheckCoroutine = StartCoroutine(CheckTimerCo());
    }
    #endregion // 함수

    #region 머터리얼
    /** 머터리얼의 렌더링 모드를 설정한다 */
    private void SetMaterialRenderingMode(Material material, float mode, int renderQueue)
    {
        // 0 = Opaque, 1 = Cutout, 2 = Fade, 3 = Transparent

        material.SetFloat("_Mode", mode);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = renderQueue;
    }

    /** 머터리얼의 렌더링 모드를 투명으로 변경한다 */
    private void SetMaterialTransparent()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            foreach (Material material in meshRenderers[i].materials)
            {
                // 렌더링 모드를 설정한다
                SetMaterialRenderingMode(material, 3f, 3000);
            }
        }
    }

    /** 머터리얼의 렌더링 모드를 불투명으로 변경한다 */
    private void SetMaterialOpaque()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            foreach (Material material in meshRenderers[i].materials)
            {
                // 렌더링 모드를 설정한다
                SetMaterialRenderingMode(material, 0f, -1);
            }
        }
    }
    #endregion // 머터리얼

    #region 코루틴 
    /** 투명도를 변경하는 코루틴 */
    private IEnumerator BecomeTransparentCo()
    {
        while (true)
        {
            bool isComplete = true;

            // 모든 레더러의 알파값이 설정된 값보다 아래인지 확인하고, 필요한 경우 알파값 감소
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // 모든 렌더러의 알파값이 지정된 알파값 수치보다 높을 경우
                if (meshRenderers[i].material.color.a > MESHRENDERER_ALPHA)
                {
                    // 완료 X
                    isComplete = false;
                }

                // 알파값 감소
                Color color = meshRenderers[i].material.color;
                color.a -= Time.deltaTime;
                meshRenderers[i].material.color = color;
            }

            // 변경이 완료됐을 경우
            if (isComplete)
            {
                // 타이머를 체크한다
                CheckTimer();
                break;
            }

            // 대기
            yield return delay;
        }
    }

    /** 초기 투명도 상태로 리셋하는 코루틴 */
    private IEnumerator ResetOriginalTransparentCo()
    {
        // 오브젝트 투명 X
        IsTransparent = false;

        while (true)
        {
            bool isComplete = true;

            // 모든 렌더러의 알파값이 1 이상인지 확인하고, 필요한 경우 알파값을 증가시킨다.
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // 모든 렌더러의 알파값이 1 이하일 경우
                if (meshRenderers[i].material.color.a < 1f)
                {
                    // 완료 X
                    isComplete = false;
                }

                // 알파값 증가
                Color color = meshRenderers[i].material.color;
                color.a += Time.deltaTime;
                meshRenderers[i].material.color = color;
            }

            // 변경이 완료되었을 경우
            if (isComplete)
            {
                // 리셋을 종료
                isReseting = false;
                break;
            }

            // 대기
            yield return resetDelay;
        }
    }

    /** 타이머를 체크하는 코루틴 */
    private IEnumerator CheckTimerCo()
    {
        // 타이머 초기화
        timer = 0f;

        while (true)
        {
            // 타이머 증가
            timer += Time.deltaTime;

            // 타이머가 지정된 값을 초과할 경우
            if (timer > MAX_TIMER)
            {
                // 리셋 시작
                isReseting = true;

                // 초기 투명도 상태로 리셋한다
                ResetOriginalTransparent();
                break;
            }

            yield return null;
        }
    }
    #endregion // 코루틴 
}