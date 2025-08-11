using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;
using Unity.VisualScripting;

public class ContractManager : MonoBehaviour
{
    [SerializeField] GameObject _contractObject, _dealObject, _recusedObject;
    [SerializeField] private List<ContractAssets> assets = new List<ContractAssets>();

    public void Start()
    {
        _contractObject.transform.DOLocalMoveY(-943f, 0f);
        _dealObject.transform.DOLocalMoveZ(-10, 1f);
        _recusedObject.transform.DOLocalMoveZ(-10, 1f);
    }

    public void ShowContract()
    {
        DOActivateAnim();

        assets[0].contract.SetActive(true);
    }

    public void ShowSignature()
    {
        assets[0].signature.SetActive(true);
    }

    public void HideContract()
    {
        DODeactivateAnim();

        StartCoroutine(WaitToHideContract());
    }

    public void PlaySuccessVFX()
    {
        assets[0].successStamp.gameObject.SetActive(true);
        assets[0].successStampVFX.Play();

        DODealAnim();
    }

    public void PlayFailVFX()
    {
        assets[0].failStamp.gameObject.SetActive(true);
        assets[0].failStampVFX.Play();

        DORecusedAnim();
    }

    private void DOActivateAnim()
    {
        _contractObject.transform.DOLocalMoveY(-26f, 0.5f).SetEase(Ease.OutBounce);
    }

    private void DODeactivateAnim()
    {
        _contractObject.transform.DOLocalMoveY(-943f, 0.5f);
    }

    private void DODealAnim()
    {
        _dealObject.SetActive(true);
        _dealObject.transform.DOLocalMoveZ(0, 2f);
    }

    private void DORecusedAnim()
    {
        _recusedObject.SetActive(true);
        _recusedObject.transform.DOLocalMoveZ(0, 2f);
    }

    private IEnumerator WaitToHideContract()
    {
        yield return new WaitForSeconds(1f);

        foreach (var asset in assets)
        {
            asset.contract.SetActive(false);
            asset.signature.SetActive(false);

            asset.failStamp.SetActive(false);
            asset.successStamp.SetActive(false);

            asset.successStampVFX.Stop();
            asset.failStampVFX.Stop();
        }

        _dealObject.SetActive(false);
        _recusedObject.SetActive(false);
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