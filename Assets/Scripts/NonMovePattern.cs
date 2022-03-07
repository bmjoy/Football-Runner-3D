using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMovePattern : MonoBehaviour, IMovable
{
    public void KillTween()
    {

    }

    public IEnumerator Move()
    {

        yield return null;
    }
}