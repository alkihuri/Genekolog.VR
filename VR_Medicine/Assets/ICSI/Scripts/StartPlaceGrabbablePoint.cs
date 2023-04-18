using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class StartPlaceGrabbablePoint : MonoBehaviour
{
    [SerializeField] private Grabbable grabbable;
    [SerializeField] private PlacePoint place;

    private IEnumerator Start()
    {
        yield return null;
        place.Place(grabbable);
        //grabbable.SetPlacePoint(this);
        Destroy(this);
    }
}
