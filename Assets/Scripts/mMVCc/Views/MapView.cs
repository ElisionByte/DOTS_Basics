namespace aMVCc.Views
{
    public class MapView : BaseElement
    {
        private void FixedUpdate()
        {
            Mediator.Controller.OnNotify(ControllerType.MapController, MapControllerActions.Update);
        }
        private void OnDestroy()
        {
            Mediator.Controller.OnNotify(ControllerType.MapController, MapControllerActions.Destroy);
        }
    }
}