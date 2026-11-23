
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CameraFollow cameraFollow;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        cameraFollow = GetComponent<CameraFollow>();
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(
            ShakeCoroutine(duration, magnitude)
        );
    }

    private IEnumerator ShakeCoroutine(
        float duration,
        float magnitude
    )
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            if (cameraFollow != null)
            {
                cameraFollow.SetShakeOffset(
                    new Vector3(x, y, 0f)
                );
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (cameraFollow != null)
            cameraFollow.SetShakeOffset(Vector3.zero);

        shakeCoroutine = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Shake(0.3f, 0.15f);
    }

}
