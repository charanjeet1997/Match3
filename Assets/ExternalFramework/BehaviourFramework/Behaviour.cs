using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour<U> : MonoBehaviour where U:MonoBehaviour
{
    public abstract void Start(U u);
    public abstract void End(U u);
    public abstract void Run(U u);
}
