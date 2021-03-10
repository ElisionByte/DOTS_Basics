using UnityEngine;

namespace Observers
{
    public class Observer : MonoBehaviour
    {
        public float delta;
        public ObserverActionType actionType;
        public ObserverType objectType;
    }
    public enum ObserverType
    {
        TransformChanger = 0,
        Spawner  = 1
    }
    public enum ObserverActionType
    {
        Y_Rotator = 0,
        X_Rotator = 1,
        Reverse_Y_Rotator = 2,
        Reverse_X_Rotator = 3,
        Z_Rotator = 4,
    }
}