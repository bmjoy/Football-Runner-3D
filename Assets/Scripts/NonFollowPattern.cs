using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonFollowPattern : MonoBehaviour, IFollowable
{
    public IEnumerator Follow()
    {

        yield return null;
    }

    public void KillTween()
    {

    }
}