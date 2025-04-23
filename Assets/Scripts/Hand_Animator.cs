using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class R_Hand_Animator : MonoBehaviour
{

    [SerializeField] private NearFarInteractor nearFarInteractor;
    [SerializeField] private SkinnedMeshRenderer handMesh;
    [SerializeField] private GameObject handArmature;
    [SerializeField] private InputActionReference selectActionRef;
    [SerializeField] private InputActionReference activateActionRef;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private float actionDelay = 0.3f;

    private static readonly int activateAnim = Animator.StringToHash("activate");
    private static readonly int selectAnim = Animator.StringToHash("select");

    private bool emptyHand;

    private void Awake()
    {
        emptyHand = true;
        nearFarInteractor.selectEntered.AddListener(OnGrab);
        nearFarInteractor.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs arg0)
    {
        Debug.Log("Selected");
        emptyHand = false;
        StartCoroutine(DelayedGrab());
    }

    private void OnRelease(SelectExitEventArgs arg0)
    {
        StartCoroutine(DelayedRelease());
    }
    private IEnumerator DelayedGrab()
    {
        yield return new WaitForSeconds(actionDelay);
        handMesh.enabled = false;
        handArmature.SetActive(false);
    }
    private IEnumerator DelayedRelease()
    {
        yield return new WaitForSeconds(actionDelay);
        handMesh.enabled = true;
        handArmature.SetActive(true);
        emptyHand = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (emptyHand)
        {
            handAnimator.SetFloat(activateAnim, activateActionRef.action.ReadValue<float>());
            handAnimator.SetFloat(selectAnim, selectActionRef.action.ReadValue<float>());
        }
    }
}
