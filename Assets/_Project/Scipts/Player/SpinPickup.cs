using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPickup : MonoBehaviour
{

    #region VARIABLES

    public GameObject MagnetContainer;
    public GameObject ShieldContainer;
    private const float SPINSPEED = 300f;

    #endregion

    #region UNITY METHODS

    private void OnEnable()
    {
        EventsManager.Instance.PickUpEvent += SpinPickUps;
        MagnetContainer.SetActive(false);
        ShieldContainer.SetActive(false);
    }
    
    private void OnDisable()
    {
        EventsManager.Instance.PickUpEvent -= SpinPickUps;
        MagnetContainer.SetActive(false);
        ShieldContainer.SetActive(false);
    }


    #endregion

    #region EVENT METHOD

    public void ManageContainer(EventsManager.PickUpType pickUpType, bool isEnabled)
    {
        switch (pickUpType)
        {
            case EventsManager.PickUpType.Magnet:
                MagnetContainer.SetActive(isEnabled);

                break;
            case EventsManager.PickUpType.Shield:
                ShieldContainer.SetActive(isEnabled);

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pickUpType), pickUpType, null);
        }
    }
    
    private void SpinPickUps(EventsManager.PickUpType pickUpType, bool isSpinning)
    {
        switch (pickUpType)
        {
            case EventsManager.PickUpType.Magnet:
                MagnetContainer.transform.RotateAround(transform.position, Vector3.up, SPINSPEED * Time.deltaTime);
                break;
            case EventsManager.PickUpType.Shield:
                ShieldContainer.transform.RotateAround(transform.position, Vector3.up, SPINSPEED * Time.deltaTime);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pickUpType), pickUpType, null);
        }
    }

    #endregion
    
}
