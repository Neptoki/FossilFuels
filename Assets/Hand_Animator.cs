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
    [SerializeField] private InputActionReference selectActionRef;
    [SerializeField] private InputActionReference activateActionRef;
    [SerializeField] private Animator handAnimator;

    private static readonly int activateAnim = Animator.StringToHash("activate");
    private static readonly int selectAnim = Animator.StringToHash("select");

    private void Awake()
    {
        nearFarInteractor.selectEntered.AddListener(OnGrab);
        nearFarInteractor.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs arg0)
    {
        Debug.Log("Selected");
        handMesh.enabled = false;
    }

    private void OnRelease(SelectExitEventArgs arg0)
    {
        handMesh.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        handAnimator.SetFloat(activateAnim, activateActionRef.action.ReadValue<float>());
        handAnimator.SetFloat(selectAnim, selectActionRef.action.ReadValue<float>());
    }
}
