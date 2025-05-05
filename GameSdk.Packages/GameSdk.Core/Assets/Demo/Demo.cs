using System;
using GameSdk.Core.Essentials;
using GameSdk.Core.Prices;
using GameSdk.Core.Toolbox;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private SerializedType _serializedType1;
    [SerializeField] private Prices _serializedType2;
    [SerializeField, SerializedTypeDropdown(typeof(IPriceData))]
    private SerializedType _serializedType3;
    [SerializeField] private Prices _serializedType4;
    [SerializeField] private Prices _serializedType5;

    [SerializeReference, SerializeReferenceDropdown(typeof(IPriceData))]
    private IPriceData[] _priceDataArray;

    [SerializeReference, SerializeReferenceDropdown("", false, typeof(IPriceData))]
    private IPriceData[] _priceDataArray2;
}

public enum Prices
{
    Price1,
    Price2
}

[Serializable]
public class PriceData1 : IPriceData
{
}

[Serializable]
public class PriceData2 : IPriceData
{
}

[Serializable]
public class PriceData3 : IPriceData
{
}
