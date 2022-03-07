using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTouchMovePattern : MonoBehaviour, ITouchMovable
{
    public void KillTween()
    {

    }

    public IEnumerator TouchMove()
    {

        yield return null;
    }
}
