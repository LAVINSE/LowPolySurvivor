using System.Collections;
using System.Linq;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    #region ����
    // ������Ʈ ������ ����
    private MeshRenderer[] meshRenderers;

    // ��ȭ�ϴ� �ӵ� �� �ʱ�ȭ �����ӵ��� ��Ÿ���� ����
    private WaitForSeconds delay = new WaitForSeconds(0.001f);
    private WaitForSeconds resetDelay = new WaitForSeconds(0.005f);

    // ������ ������ ���� ��
    private const float MESHRENDERER_ALPHA = 0.25f;
    private const float MAX_TIMER = 0.5f;

    // ���������� Ȯ���ϴ� ����
    private bool isReseting = false;

    // Ÿ�̸� ����
    private float timer = 0f;

    // �ڷ�ƾ ����
    private Coroutine timeCheckCoroutine;
    private Coroutine resetCoroutine;
    private Coroutine becomeTransparentCoroutine;
    #endregion // ����

    #region ������Ƽ
    public bool IsTransparent { get; private set; } = false; // ������Ʈ�� �������� Ȯ���ϴ� ����
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        meshRenderers = GetComponents<MeshRenderer>();
    }

    /** ������Ʈ�� �����ϰ� ����� */
    public void BecomeTransparent()
    {
        // �̹� ������Ʈ�� ������ ���
        if (IsTransparent)
        {
            // Ÿ�̸� �ʱ�ȭ, �Լ� ����
            timer = 0f;
            return;
        }

        // �ڷ�ƾ�� �����ϰ�, �������� ���
        if (resetCoroutine != null && isReseting)
        {
            // ���� ����
            isReseting = false;
            IsTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        // ������ �����ϰ� �ڷ�ƾ�� ����
        SetMaterialTransparent();
        IsTransparent = true;
        becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCo());
    }

    /** �ʱ� ���� ���·� �����Ѵ� */
    public void ResetOriginalTransparent()
    {
        // ���͸����� ������ ��带 ���������� �����Ѵ�
        SetMaterialOpaque();

        // �ʱ� ���� ���·� �����ϴ� �ڷ�ƾ�� �����Ѵ�
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCo());
    }

    /** Ÿ�̸Ӹ� üũ�Ѵ� */
    public void CheckTimer()
    {
        // Ÿ�̸� üũ �ڷ�ƾ�� ������ ���
        if (timeCheckCoroutine != null)
        {
            // �������̴� Ÿ�̸� üũ �ڷ�ƾ�� �����Ѵ�
            StopCoroutine(timeCheckCoroutine);
        }

        // Ÿ�̸� üũ �ڷ�ƾ�� �����Ѵ�
        timeCheckCoroutine = StartCoroutine(CheckTimerCo());
    }
    #endregion // �Լ�

    #region ���͸���
    /** ���͸����� ������ ��带 �����Ѵ� */
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

    /** ���͸����� ������ ��带 �������� �����Ѵ� */
    private void SetMaterialTransparent()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            foreach (Material material in meshRenderers[i].materials)
            {
                // ������ ��带 �����Ѵ�
                SetMaterialRenderingMode(material, 3f, 3000);
            }
        }
    }

    /** ���͸����� ������ ��带 ���������� �����Ѵ� */
    private void SetMaterialOpaque()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            foreach (Material material in meshRenderers[i].materials)
            {
                // ������ ��带 �����Ѵ�
                SetMaterialRenderingMode(material, 0f, -1);
            }
        }
    }
    #endregion // ���͸���

    #region �ڷ�ƾ 
    /** ������ �����ϴ� �ڷ�ƾ */
    private IEnumerator BecomeTransparentCo()
    {
        while (true)
        {
            bool isComplete = true;

            // ��� �������� ���İ��� ������ ������ �Ʒ����� Ȯ���ϰ�, �ʿ��� ��� ���İ� ����
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // ��� �������� ���İ��� ������ ���İ� ��ġ���� ���� ���
                if (meshRenderers[i].material.color.a > MESHRENDERER_ALPHA)
                {
                    // �Ϸ� X
                    isComplete = false;
                }

                // ���İ� ����
                Color color = meshRenderers[i].material.color;
                color.a -= Time.deltaTime;
                meshRenderers[i].material.color = color;
            }

            // ������ �Ϸ���� ���
            if (isComplete)
            {
                // Ÿ�̸Ӹ� üũ�Ѵ�
                CheckTimer();
                break;
            }

            // ���
            yield return delay;
        }
    }

    /** �ʱ� ���� ���·� �����ϴ� �ڷ�ƾ */
    private IEnumerator ResetOriginalTransparentCo()
    {
        // ������Ʈ ���� X
        IsTransparent = false;

        while (true)
        {
            bool isComplete = true;

            // ��� �������� ���İ��� 1 �̻����� Ȯ���ϰ�, �ʿ��� ��� ���İ��� ������Ų��.
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                // ��� �������� ���İ��� 1 ������ ���
                if (meshRenderers[i].material.color.a < 1f)
                {
                    // �Ϸ� X
                    isComplete = false;
                }

                // ���İ� ����
                Color color = meshRenderers[i].material.color;
                color.a += Time.deltaTime;
                meshRenderers[i].material.color = color;
            }

            // ������ �Ϸ�Ǿ��� ���
            if (isComplete)
            {
                // ������ ����
                isReseting = false;
                break;
            }

            // ���
            yield return resetDelay;
        }
    }

    /** Ÿ�̸Ӹ� üũ�ϴ� �ڷ�ƾ */
    private IEnumerator CheckTimerCo()
    {
        // Ÿ�̸� �ʱ�ȭ
        timer = 0f;

        while (true)
        {
            // Ÿ�̸� ����
            timer += Time.deltaTime;

            // Ÿ�̸Ӱ� ������ ���� �ʰ��� ���
            if (timer > MAX_TIMER)
            {
                // ���� ����
                isReseting = true;

                // �ʱ� ���� ���·� �����Ѵ�
                ResetOriginalTransparent();
                break;
            }

            yield return null;
        }
    }
    #endregion // �ڷ�ƾ 
}