using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public bool shaking;
    public IEnumerator Shake (float duration)
	{
		shaking = true;

		Vector3 originalPos = transform.localPosition;

		float elapsed = 0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-0.015f, 0.015f);
			float y = Random.Range(0.585f, 0.615f);

			transform.localPosition = new Vector3(x, y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = originalPos;

		shaking = false;
	}
}
