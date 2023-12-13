using UnityEngine;

public abstract class InGameComponents : MonoBehaviour
{
    public abstract void EnteredState();
    public abstract void LeftState();
}