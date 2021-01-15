using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 normalPosition;
    private float elapsed;
    private float x, y;

    private void Start()
    {
        normalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        elapsed = 0.0f;

        while (elapsed < duration)
        {
            x = Random.Range(-1f, 1f) * magnitude;
            y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, normalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = normalPosition;
    }
}
