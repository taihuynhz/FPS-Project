using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSFramework
{
    public interface IOnHitInParent
    {
        void OnHitInParent(HitInfo hitInfo);
    }
}