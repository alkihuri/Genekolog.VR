using UnityEngine;

public abstract class TrayInstrument : MonoBehaviour
{
    [SerializeField] private TrayInstrumentsType type;

    public TrayInstrumentsType Type => type;
}