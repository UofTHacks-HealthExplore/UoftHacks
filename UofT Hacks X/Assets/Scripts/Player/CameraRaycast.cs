using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRaycasting : MonoBehaviour
{

    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    private Interactables currentTarget;
    private Camera mainCamera;
    private PlayerController player;

    private void Awake()
    {
        mainCamera = Camera.main;
        player = GetComponent<PlayerController>();
    }

    public void OnInteract(InputValue value)
    {
        if (currentTarget != null)
        {
            currentTarget.OnInteract();
        }
    }
    private void Update()
    {
        if (player.lockMovement)
        {
            RaycastForInteractables();
        }
    }

    private void RaycastForInteractables()
    {
        RaycastHit hit;


        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        if (Physics.Raycast(ray, out hit, range))
        {
            Interactables interactable = hit.collider.GetComponent<Interactables>();

            if (interactable != null)
            {
                if (hit.distance <= interactable.MaxRange)
                {
                    if (interactable == currentTarget)
                    {
                        return;
                    }
                    else if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                    else
                    {
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                }
                else
                {
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }

            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }
    }
}
