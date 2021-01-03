using EzySlice;

using UnityEngine;

namespace CodeBase.Logic.Weapon
{
    public class Katana : MonoBehaviour, IWeapon
    {
        private int layerID;
        private BoxCollider boxCollider;

        public void Show()
        {
            //Show sound;
        }
        public void Hide()
        {
            //Hide sound;
        }

        public void Atack()
        {
            Collider[] colliders = Physics.OverlapBox(boxCollider.gameObject.transform.position, boxCollider.size / 2, Quaternion.identity, 1 << 11);
            for (int i = 0; i < colliders.Length; i++)
            {
                GameObject tempObject = colliders[i].gameObject;
                SlicedHull slicedHull = tempObject.Slice(boxCollider.gameObject.transform.position, boxCollider.gameObject.transform.up, null);

                if (slicedHull != null)
                {
                    CreateUpperLowerHull(tempObject, slicedHull);
                }
            }
        }

        public void Setup(BoxCollider boxCollider, int layerID)
        {
            this.boxCollider = boxCollider;
            this.layerID = layerID;
        }

        private void CreateUpperLowerHull(GameObject obj, SlicedHull slicedHull)
        {
            slicedHull.CreateLowerHull(obj, null)
                .AddCollider()
                .AddRigidbody().layer = 11;
            slicedHull.CreateUpperHull(obj, null)
                .AddCollider()
                .AddRigidbody().layer = 11;

            Destroy(obj);
        }
    }
}

