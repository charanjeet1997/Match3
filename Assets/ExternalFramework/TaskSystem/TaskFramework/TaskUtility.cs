using TaskSystem.Examples;

namespace TaskSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class TaskUtility 
    {
        public static void Dispose(this Task task)
        {
            // frameworkTask.Stop();
            task = null;
        }   
    }
}