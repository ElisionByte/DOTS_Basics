using UnityEngine;
using UnityEngine.Jobs;

namespace Observers
{
    public struct ObserverData
    {
        private float delta;
        private ObserverActionType type;

        public ObserverData(Observer observer)
        {
            type = observer.actionType;
            delta = observer.delta;
        }
        public void UpdateTransformData(TransformAccess transform)
        {
            switch (type)
            {
                case ObserverActionType.Y_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(0, delta, 0);
                    }
                    break;
                case ObserverActionType.X_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(delta, 0, 0);
                    }
                    break;
                case ObserverActionType.Reverse_X_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(-delta, 0, 0);
                    }
                    break;
                case ObserverActionType.Reverse_Y_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(0, -delta, 0);
                    }
                    break;
                case ObserverActionType.Z_Rotator:
                    {
                        transform.rotation *= Quaternion.Euler(0, 0, delta);
                    }
                    break;
                default:
                    throw new System.Exception("Wrong observer action type");
            }
        }
        //public void UpdateSpawnData()
        //{

        //}
    }
}
