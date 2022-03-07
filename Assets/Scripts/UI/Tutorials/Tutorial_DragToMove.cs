using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace TFPlay.UI
{
    public class Tutorial_DragToMove : TutorialItem
    {
        [SerializeField]
        private Transform hand;
        [SerializeField]
        private Transform leftInfinityCenter;
        [SerializeField]
        private Transform rightInfinityCenter;
        [SerializeField]
        private float moveTime;

        public override void Play()
        {
            base.Play();
            StartCoroutine(Move());
        }

        public override void Stop()
        {
            StopAllCoroutines();
            base.Stop();
        }

        private IEnumerator Move()
        {
            var distanceFromFocusToCenter = Vector3.Distance(leftInfinityCenter.localPosition, rightInfinityCenter.localPosition) / 2;
            var points = BernoulliLemniscate.GeneratePoint(hand.localPosition, distanceFromFocusToCenter);
            var timePerOnePart = moveTime / points.Length;
            while (gameObject.activeSelf)
            {
                foreach (var item in points)
                {
                    hand.transform.DOLocalMove(item, timePerOnePart).SetEase(Ease.Linear);
                    yield return new WaitForSeconds(timePerOnePart);
                }
                yield return null;
            }
        }
    }

    public class BernoulliLemniscate
    {
        private const float maxRotateAngle = 6.21f;
        private const float accuracy = .1f;

        public static Vector2[] GeneratePoint(Vector3 center, float distanceFromFocusToCenter)
        {
            List<Vector2> points = new List<Vector2>();
            float curRotateAngle = 0;
            float sqr2 = Mathf.Sqrt(2);
            while (curRotateAngle < maxRotateAngle)
            {
                var cos = Mathf.Cos(curRotateAngle);
                var sin = Mathf.Sin(curRotateAngle);
                var sin_sq = Mathf.Pow(Mathf.Sin(curRotateAngle), 2) + 1;
                var x = distanceFromFocusToCenter * sqr2 * cos / sin_sq;
                var y = distanceFromFocusToCenter * sqr2 * cos * sin / sin_sq;
                curRotateAngle += accuracy;
                Vector2 result = (Vector2)center + new Vector2(x, y);
                points.Add(result);
            }
            return points.ToArray();
        }
    }
}