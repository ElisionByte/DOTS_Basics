namespace aMVCc.Models
{
    public class Model : BaseElement
    {
        public MapModel mapModel;

        public void InitialiseAll()
        {
            mapModel.Initialise();
        }
    }
}