using Mono.Cecil;
using System.Collections;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{

    public LayerMask fruitLayerMask;

    public GameObject fruitVfx;

    public float scaleDuration = 0.3f;

    private int spawnNumber = 5;

    public Vector3 targetScale = new Vector3(1f, 1f, 1f);

    public GameObject bananaPieces;
    public GameObject applePieces;
    public GameObject melonPieces;
    public GameObject berryPieces;

    private void OnCollisionEnter(Collision collision)
    {
        if ((fruitLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            EventManager.TriggerScreenShake();
            EventManager.TriggerIncreaseScore(1);
            SoundManager.Instance.PlayRandomSFX(collision.transform.position);
            Debug.Log("Collided with an object on the Fruit layer: " + collision.gameObject.name);

            Vector3 collisionPoint = collision.contacts[0].point;
            Instantiate(fruitVfx, collisionPoint, Quaternion.identity);

            StartCoroutine(ScaleObject(collision.transform, collision.gameObject));
        }
    }

    private IEnumerator ScaleObject(Transform target, GameObject collisionGameObject)
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

        ExplodeAndDespawn(target, collisionGameObject);
    }

    private void ExplodeAndDespawn(Transform target, GameObject collisionGameObject)
    {
        switch (collisionGameObject.gameObject.tag)
        {
            case "Melon":
                for (int i = 0; i < spawnNumber; i++)
                {
                    GameObject randomGameObject = melonPieces;
                    Transform randomPosition = target.transform;
                    Instantiate(randomGameObject, randomPosition.position, Quaternion.identity);
                }
                break;

            case "Apple":
                for (int i = 0; i < spawnNumber; i++)
                {
                    GameObject randomGameObject = applePieces;
                    Transform randomPosition = target.transform;
                    Instantiate(randomGameObject, randomPosition.position, Quaternion.identity);
                }
                break;

            case "Banana":
                for (int i = 0; i < spawnNumber; i++)
                {
                    GameObject randomGameObject = bananaPieces;
                    Transform randomPosition = target.transform;
                    Instantiate(randomGameObject, randomPosition.position, Quaternion.identity);
                }
                break;

            case "Berry":
                for (int i = 0; i < spawnNumber; i++)
                {
                    GameObject randomGameObject = berryPieces;

                    Transform randomPosition = target.transform;
                    Instantiate(randomGameObject, randomPosition.position, Quaternion.identity);
                }
                break;
        }
        collisionGameObject.gameObject.SetActive(false);

    }
}
