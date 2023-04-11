using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public List<BakedGood> bakedGoods;

    public Order()
    {
        bakedGoods = new List<BakedGood>();
    }
    //public List<Coffee> coffee;
}
