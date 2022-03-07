using System.Collections;
using UnityEngine;

public interface IMovable
{
    IEnumerator Move();

    void KillTween();
}