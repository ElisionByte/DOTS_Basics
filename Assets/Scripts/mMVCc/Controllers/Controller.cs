using System;

namespace aMVCc.Controllers
{
    public class Controller : BaseElement
    {
        public MapController mapController;

        public void OnNotify(ControllerType controllerType, Enum controllerAction)
        {
            switch (controllerType)
            {
                case ControllerType.MapController: 
                    { 
                        mapController.Notify(controllerAction);
                    }
                    break;
                default:
                    throw new Exception("Wrong controller type");
            }
        }
    }
}

public enum ControllerType
{
    MapController = 0
}