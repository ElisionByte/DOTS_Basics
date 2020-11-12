using UnityEngine;
using UnityEngine.Jobs;

namespace Observer
{
    public class Observer : MonoBehaviour
    {
        public float rotationSpeed;
        public ObserverType type;
    }
    public struct ObserverData
    {
        public float delta;
        public ObserverType type;
        public ObserverData(Observer observer)
        {
            delta = observer.rotationSpeed;
            type = observer.type;
        }
        public void UpdateData(TransformAccess transform)
        {
            switch (type)
            {
                case ObserverType.Y_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(0, delta, 0);
                    }
                    break;
                case ObserverType.X_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(delta, 0, 0);
                    }
                    break;
                case ObserverType.Reverse_X_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(-delta, 0, 0);
                    }
                    break;
                case ObserverType.Reverse_Y_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(0, -delta, 0);
                    }
                    break;
                case ObserverType.Z_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(0, 0, delta);
                    }
                    break;
            }
        }
    }
    public enum ObserverType
    {
        Y_Rotator = 0,
        X_Rotator = 1,
        Reverse_Y_Rotator = 2,
        Reverse_X_Rotator = 3,
        Z_Rotator = 4,
    }
}
