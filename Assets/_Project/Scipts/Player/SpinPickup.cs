using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpinPickup : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, -180, 0), 1f).SetLoops(-1);
    }
}
