using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootMotion.Demos
{
    // Adds simple FK rotation offset to bones.
    public class FKOffset : MonoBehaviour
    {
        private const float MOVE_SPEED = 25f;

        [System.Serializable]
        public class Offset
        {
            [HideInInspector] public string name;
            public HumanBodyBones bone;
            public Vector3 rotationOffset;

            private Transform t;

            public void Apply(Animator animator)
            {
                if (t == null) t = animator.GetBoneTransform(bone);
                if (t == null) return;

                t.localRotation *= Quaternion.Euler(rotationOffset);
            }
        }

        public Offset[] offsets;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            foreach (Offset offset in offsets)
            {
                offset.Apply(animator);
            }

            DecreaseWithTime();
        }


        private void DecreaseWithTime()
        {
            foreach (Offset offset in offsets)
            {
                var vec = offset.rotationOffset;

                offset.rotationOffset = DecreaseChecks(vec);
            }
        }


        private Vector3 DecreaseChecks(Vector3 vec)
        {
            if (vec.x > 1)
                vec.x -= MOVE_SPEED * Time.deltaTime;

            if (vec.y > 1)
                vec.y -= MOVE_SPEED * Time.deltaTime;

            if (vec.z > 1)
                vec.z -= MOVE_SPEED * Time.deltaTime;

            if (vec.x < 1)
                vec.x += MOVE_SPEED * Time.deltaTime;

            if (vec.y < 1)
                vec.y += MOVE_SPEED * Time.deltaTime;

            if (vec.z < 1)
                vec.z += MOVE_SPEED * Time.deltaTime;

            return vec;
        }


        private void OnDrawGizmosSelected()
        {
            foreach (Offset offset in offsets)
            {
                offset.name = offset.bone.ToString();
            }
        }
    }
}
