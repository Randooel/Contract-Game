using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using System.Net.Http.Headers;
using UnityEditor;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ContractManager : MonoBehaviour
{
    private CurrentClient _currentClient;
    private ClientManager _clientManager;

    [Header("Contract and Stamps")]
    [SerializeField] GameObject _contractObject;
    [SerializeField] GameObject _dealObject;
    [SerializeField] GameObject _recusedObject;

    [Space(10)]
    [SerializeField] private List<ContractAssets> assets = new List<ContractAssets>();

    [Space(10)]
    [SerializeField] private GameObject _contractField;

    [Header("Request")]
    [SerializeField] private TextMeshProUGUI _requestDescription;
    [SerializeField] private Image _requestImage;
    [SerializeField] private List<GameObject> _requestField = new List<GameObject>();

    [Header("Price")]
    [SerializeField] private TextMeshProUGUI _priceDescription;
    [SerializeField] private Image _priceImage;
    [SerializeField] private List<GameObject> _priceField = new List<GameObject>();



    public void Start()
    {
        _currentClient = FindObjectOfType<CurrentClient>();
        _clientManager = GetComponent<ClientManager>();

        _contractObject.transform.DOLocalMoveY(-943f, 0f);
        _dealObject.transform.DOLocalMoveZ(-10, 1f);
        _recusedObject.transform.DOLocalMoveZ(-10, 1f);
    }

    public void SetPossiblePrices()
    {
        var cc = _currentClient.possessions;

        Debug.Log($"names count: {cc.names?.Count}");

        // First Client's info
        foreach (var name in cc.names)
        {
            Debug.Log("Foreach");

            GameObject fieldInstance = Instantiate(_contractField);

            TMPro.TextMeshProUGUI description = fieldInstance.transform.Find("Field Description").GetComponent<TMPro.TextMeshProUGUI>();
            description.text = name;

            _priceField.Add(fieldInstance);
        }

        // Then Client's possessions
    }

    public void ShowPrices()
    {
        foreach(var price in _priceField)
        {

        }
    }

    public void ShowRequests()
    {
        foreach(var request in _requestField)
        {

        }
    }

    public void InstantiateField()
    {
        var cc = _currentClient.possessions;

        GameObject fieldInstance = Instantiate(_contractField);

        TMPro.TextMeshProUGUI description = fieldInstance.transform.Find("Field Description").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetPrice()
    {

    }

    // ANIMATIONS
    public void ShowContract()
    {
        DOActivateAnim();

        ShowRequest();

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

    public void ShowRequest()
    {
        /*
        _requestDescription.text = _currentClient.objectiveDescription;
        _requestImage.sprite = _currentClient.objectiveSprite;
        */
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
        _dealObject.transform.DOLocalMoveZ(0, 10f);
    }

    private void DORecusedAnim()
    {
        _recusedObject.SetActive(true);
        _recusedObject.transform.DOLocalMoveZ(0, 4f);
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

// [x]
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