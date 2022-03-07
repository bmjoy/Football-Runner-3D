using System.Collections;
using UnityEngine;

public interface ITouchMovable
{
    IEnumerator TouchMove();

    void KillTween();
}