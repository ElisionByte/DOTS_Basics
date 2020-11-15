using UnityEngine;

namespace aMVCc
{
    public class GameMediator : Mediator
    {
        private void Start()
        {
            Application.targetFrameRate = 120;
            base.Model.InitialiseAll();
        }
    }
}
