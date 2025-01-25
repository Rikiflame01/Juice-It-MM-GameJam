using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arms : MonoBehaviour
{

    public Rigidbody LftArm;
    public Rigidbody RhtArm;

    public Transform LftArmTransform;
    public Transform RhtArmTransform;

    public Transform travelPoint;
    private Transform clickedTransform;
    public float moveSpeed = 0.5f;

    private bool moveToTargetRht;
    private bool moveToTargetLft;

    private bool isResettingLft = false;
    private bool isResettingRht = false;

    public Camera mainCamera;

    private PlayerInput playerInput;

    private void Awake()
    {
        LftArmTransform.position = LftArm.position;
        RhtArmTransform.position = RhtArm.position;

        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void Update()
    {
        if (moveToTargetRht != false && clickedTransform != null)
        {
            RhtArm.constraints = RigidbodyConstraints.None;
            RhtArm.MovePosition(Vector3.Lerp(RhtArm.position, clickedTransform.position, moveSpeed * Time.deltaTime));

            StartCoroutine(ResetPositionRht());
        }
        if (moveToTargetLft != false && clickedTransform != null)
        {
            LftArm.constraints = RigidbodyConstraints.None;
            LftArm.MovePosition(Vector3.Lerp(LftArm.position, clickedTransform.position, moveSpeed * Time.deltaTime));

            StartCoroutine(ResetPositionLft());
        }

    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log("Context");
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickedTransform = hit.transform;
                moveToTargetLft = true;
                Debug.Log("Hit position: " + hit.point);
            }

            Debug.Log("Left click");   
        }

    }


    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickedTransform = hit.transform;
                moveToTargetRht = true;
                Debug.Log("Hit position: " + hit.point);
            }

        }
    }

    private IEnumerator ResetPositionLft()
    {
        if (isResettingLft == false)
        {
            isResettingLft = true;
            yield return new WaitForSeconds(1);
            moveToTargetLft = false;

            LftArm.position = LftArmTransform.position;

            yield return new WaitForSeconds(1f);
            ResetMomentum(LftArm);

            isResettingLft = false;
        }
    }
    private IEnumerator ResetPositionRht()
    {
        if (isResettingRht == false)
        {
            isResettingRht = true;
            yield return new WaitForSeconds(1);
            moveToTargetRht = false;

            RhtArm.position = RhtArmTransform.position;
            yield return new WaitForSeconds(1f);
            ResetMomentum(RhtArm);
            isResettingRht = false;
        }
    }
    private void ResetMomentum(Rigidbody rb)
    {
        Vector3 targetRotation = new Vector3(130, 0, 0);
        rb.rotation = Quaternion.Euler(targetRotation);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
