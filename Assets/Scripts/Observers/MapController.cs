namespace aMVCc.Controllers
{
    public class MapController : BaseElement
    {
        public void Notify(System.Enum action)
        {
            switch ((MapControllerActions)action)
            {
                case MapControllerActions.Update:
                    {
                        if (Mediator.Model.mapModel.IsLevelLoaded)
                            Mediator.Model.mapModel.Sublect.Notify();
                    }
                    break;
                case MapControllerActions.Destroy:
                    {
                        Mediator.Model.mapModel.Sublect.Detach();
                    }
                    break;
                default:
                    throw new System.Exception("Wrong controller actionType");
            }
        }
    }
}

public enum MapControllerActions
{
    Update = 0,
    Destroy = 1
}