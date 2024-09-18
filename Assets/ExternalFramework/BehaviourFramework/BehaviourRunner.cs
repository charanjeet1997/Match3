using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourRunner<T> where T:MonoBehaviour
{
    T owner;
    private List<Behaviour<T>> behaviours; 
    private int behaviourCount;
    public BehaviourRunner(T t,params Behaviour<T>[] targetBehaviours)
    {
        owner=t;
        behaviours=new List<Behaviour<T>>();
        behaviourCount=0;
        Add(targetBehaviours);
    }
    public void Add(Behaviour<T> behaviour)
    {
        if(behaviours.Contains(behaviour))
            return;
        behaviour.Start(owner);
        behaviours.Add(behaviour);
        behaviourCount++;
       
    }
    public void Add(params Behaviour<T>[] targetBehaviours)
    {
        for(int indexOfBehaviour=0;indexOfBehaviour<targetBehaviours.Length;indexOfBehaviour++)
        {
            if(!behaviours.Contains(targetBehaviours[indexOfBehaviour]))
            {
                targetBehaviours[indexOfBehaviour].Start(owner);
                behaviours.Add(targetBehaviours[indexOfBehaviour]);
                behaviourCount++;
            }
        }
    }
    public void Remove(params Behaviour<T>[] targetBehaviours)
    {
        for(int indexOfBehaviour=0;indexOfBehaviour<targetBehaviours.Length;indexOfBehaviour++)
        {
            if(behaviours.Contains(targetBehaviours[indexOfBehaviour]))
            {
                behaviours.Remove(targetBehaviours[indexOfBehaviour]);
                targetBehaviours[indexOfBehaviour].End(owner);
                behaviourCount--;
            }
        }
    }
    public void Remove(Behaviour<T> behaviour)
    {
        if(!behaviours.Contains(behaviour))
            return;
        behaviourCount--;
        behaviours.Remove(behaviour);
        behaviour.End(owner);
       
    }
    public void Run()
    {
        for(int indexOfBehaviour=0;indexOfBehaviour<behaviourCount;indexOfBehaviour++)
        {
            behaviours[indexOfBehaviour].Run(owner);        
        }
    }
}
