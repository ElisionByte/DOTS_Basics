using EzySlice;

using UnityEngine;

namespace CodeBase.Logic.Weapon
{
    public class Katana : MonoBehaviour, IWeapon
    {
        public ParticleSystem Splash;
        public BoxCollider boxCollider;

        private int layerID;


        public void Show()
        {
            //Show actions;
        }
        public void Hide()
        {
            //Hide actions;
        }

        public void Atack()
        {
            Splash.gameObject.SetActive(true);
            Splash.Play();
            Collider[] colliders = Physics.OverlapBox(boxCollider.gameObject.transform.position, boxCollider.size, Quaternion.identity, 1 << 12);
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

        //public void Setup(BoxCollider boxCollider, int layerID)
        //{
        //    this.boxCollider = boxCollider;
        //    this.layerID = layerID;
        //}

        private void CreateUpperLowerHull(GameObject obj, SlicedHull slicedHull)
        {
            slicedHull.CreateLowerHull(obj, null)
                .AddCollider()
                .AddRigidbody().layer = 12;
            slicedHull.CreateUpperHull(obj, null)
                .AddCollider()
                .AddRigidbody().layer = 12;

            Destroy(obj);
        }
    }
}

