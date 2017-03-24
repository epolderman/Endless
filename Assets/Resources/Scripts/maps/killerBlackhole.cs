using UnityEngine;
using System.Collections;

public class killerBlackhole : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponentInParent<Rigidbody2D>().isKinematic = true;
        if ( other.gameObject.GetComponentInParent<Health_Statistics>() != null)
            StartCoroutine(LerpFunction(.3f, other));
    }

    IEnumerator LerpFunction(float time, Collider2D other)
    {
        Vector3 originalScale = other.gameObject.transform.parent.transform.localScale;
        Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f);
        float originalTime = time;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;

            other.gameObject.transform.parent.transform.localScale = Vector3.Lerp(targetScale, originalScale, time / originalTime);
            yield return null;
        }

        other.gameObject.GetComponentInParent<Health_Statistics>().handle_Death();

    }
}
