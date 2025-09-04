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
    [SerializeField][Range(0, 50)] private float distanceBetweenFields;

    [Header("Request")]
    [SerializeField] private TextMeshProUGUI _requestDescription;
    [SerializeField] private Image _requestImage;
    [SerializeField] private List<GameObject> _requestField = new List<GameObject>();
    [SerializeField] private Transform _requestTransform;

    [Header("Price")]
    [SerializeField] private TextMeshProUGUI _priceDescription;
    [SerializeField] private Image _priceImage;
    [SerializeField] private List<GameObject> _priceField = new List<GameObject>();
    [SerializeField] private Transform _priceTransform;


    public void Start()
    {
        _currentClient = FindObjectOfType<CurrentClient>();
        _clientManager = GetComponent<ClientManager>();

        _contractObject.transform.DOLocalMoveY(-943f, 0f);
        _dealObject.transform.DOLocalMoveZ(-10, 1f);
        _recusedObject.transform.DOLocalMoveZ(-10, 1f);
    }

    // if setPrice is false PRICES will be setted. Else REQUEST
    public void SetPossibleValues(bool setPrice)
    {
        var cc = _currentClient.possessions;
        var field = _priceField;
        var transformParent = _priceTransform;
        if (!setPrice)
        {
            cc = _currentClient.objectives;
            field = _requestField;
            transformParent = _requestTransform;
        }

        // First Client's info


        // Then Client's possessions
        foreach (var name in cc.names)
        {
            var fieldInstance = InstantiateField(setPrice, transformParent);

            // Updating text
            ChangeText(fieldInstance, name);

            field.Add(fieldInstance);
        }

        foreach(var visual in cc.visuals)
        {
            var fieldInstance = InstantiateField(setPrice,transformParent);

            // Updating text
            ChangeText(fieldInstance, visual.name);

            // Updating image
            ChangeSprite(fieldInstance, visual);

            field.Add(fieldInstance);
        }

        // ADD CASH SPRITE
        // CASH
        if(cc.cash != 0)
        {
            var fieldInstance = InstantiateField(setPrice, transformParent);
            ChangeText(fieldInstance, cc.description);

            field.Add(fieldInstance);
        }

        if(cc.useStatus)
        {
            var sat = InstantiateField(setPrice, transformParent);
            ChangeText(sat, "Satisfaction: " + _currentClient.satisfaction.ToString());

            field.Add(sat);

            var res = InstantiateField(setPrice,transformParent);
            ChangeText(res, "Resolution: " + _currentClient.satisfaction.ToString());

            field.Add(res);
        }

        foreach(var character in cc.characters)
        {
            var fieldInstance = InstantiateField(setPrice, transformParent);
            ChangeText(fieldInstance, cc.description);
            ChangeSprite(fieldInstance, character.fullSprite);

            field.Add(fieldInstance);
        }

        foreach(var item in cc.items)
        {
            var fieldInstance = InstantiateField(setPrice, transformParent);
            ChangeText(fieldInstance, cc.description);
            ChangeSprite(fieldInstance, item.sprite);

            field.Add(fieldInstance);
        }
    }

    private GameObject InstantiateField(bool isPrice, Transform parent)
    {
        GameObject fieldInstance = Instantiate(_contractField, parent, false);
        fieldInstance.transform.localScale = Vector3.one;

        var qtd = _priceField.Count;
        if(!isPrice)
        {
            qtd = _requestField.Count;
        }

        fieldInstance.transform.localPosition = new Vector3(0f, -distanceBetweenFields, 0f) * qtd;

        return fieldInstance;
    }

    private string ChangeText(GameObject instance, string text)
    {
        TMPro.TextMeshProUGUI description = instance.transform.Find("Field Description").GetComponent<TMPro.TextMeshProUGUI>();
        description.text = text;

        return description.text;
    }

    private Sprite ChangeSprite(GameObject instance, Sprite sprite)
    {
        Image imageField = instance.transform.Find("Field Image").GetComponent<Image>();
        imageField.sprite = sprite;

        return imageField.sprite;
    }

    public void ShowValues(bool showPrice, bool showRequest)
    {
        if(showPrice)
        {
            foreach (var price in _priceField)
            {
                price.SetActive(true);
            }
        }
        
        if(showRequest)
        {
            foreach (var request in _requestField)
            {
                request.SetActive(true);
            }
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