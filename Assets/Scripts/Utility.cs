using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static bool CheckLayer(LayerMask targetLayer, int targetVal){
        return (targetLayer.value & (1 << targetVal)) > 0;
    }
}
