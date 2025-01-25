using System.Collections;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{

    public LayerMask fruitLayerMask;

    public GameObject fruitVfx;

    public float scaleDuration = 0.3f;

    public Vector3 targetScale = new Vector3(1f, 1f, 1f);

    private void OnCollisionEnter(Collision collision)
    {
        if ((fruitLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            SoundManager.Instance.PlayRandomSFX(collision.transform.position);
            Debug.Log("Collided with an object on the Fruit layer: " + collision.gameObject.name);

            Vector3 collisionPoint = collision.contacts[0].point;
            Instantiate(fruitVfx, collisionPoint, Quaternion.identity);

            StartCoroutine(ScaleObject(collision.transform));
        }
    }

    private IEnumerator ScaleObject(Transform target)
    {
        Vector3 originalScale = target.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            target.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;

        elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            target.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = originalScale;
    }
}
