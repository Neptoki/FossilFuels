using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabAnimator : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer handModel;

    // Start is called before the first frame update
    void Start()
    {
        handModel.enabled = false;
    }

    public void selectEnter()
    {
        handModel.enabled = true;
    }
    public void selectExit()
    {
        handModel.enabled = false;
    }
}
