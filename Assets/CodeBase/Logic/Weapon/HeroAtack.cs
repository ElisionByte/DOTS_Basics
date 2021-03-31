using UnityEngine;

namespace CodeBase.Logic.Weapon
{
    public class HeroAtack:MonoBehaviour
    {
        public Katana Katana;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Katana.Atack();
            }
        }
    }
}
