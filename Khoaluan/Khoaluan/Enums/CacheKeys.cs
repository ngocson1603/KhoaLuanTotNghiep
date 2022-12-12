﻿using System;
namespace Khoaluan.Enums
{
    public enum CacheKeys
    {
        Categories
    }
    public enum marketType
    {
        order=0,
        sell=1,
        buy=2,  
        soldout=3,
        purchased=4,
        paypal = 5
    }
    public enum productType
    {
        pending=0,
        accept=1,
        decline=2,
        release=3,
    }
    public enum RefundType
    {
        pending=0,
        accept=1,
        decline=2
    }
}
