using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowable
{
    IEnumerator Follow();

    void KillTween();
}