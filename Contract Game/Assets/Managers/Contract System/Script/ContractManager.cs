using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ContractManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private List<ContractAssets> assets = new List<ContractAssets>();

    public void ShowContract()
    {
        _animator.SetTrigger("activate");

        assets[0].contract.SetActive(true);
    }

    public void ShowSignature()
    {
        assets[0].signature.SetActive(true);
    }

    public void HideContract()
    {
        _animator.SetTrigger("deactivate");

        foreach (var asset in assets)
        {
            asset.contract.SetActive(false);
            asset.signature.SetActive(false);

            asset.failStamp.SetActive(false);
            asset.successStamp.SetActive(false);

            asset.successStampVFX.Stop();
            asset.failStampVFX.Stop();
        }
    }

    public void PlaySuccessVFX()
    {
        assets[0].successStamp.gameObject.SetActive(true);
        assets[0].successStampVFX.Play();
    }

    public void PlayFailVFX()
    {
        assets[0].failStamp.gameObject.SetActive(true);
        assets[0].failStampVFX.Play();
    }
}

[System.Serializable]
public class ContractAssets
{
    public GameObject contract;
    public GameObject signature;

    public GameObject successStamp;
    public GameObject failStamp;

    public VisualEffect successStampVFX;
    public VisualEffect failStampVFX;
}
