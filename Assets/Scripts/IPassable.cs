using UnityEngine;
using System.Collections;

public interface IPassable
{
    IEnumerator Pass(Character entity);
}
