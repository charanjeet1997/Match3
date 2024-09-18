using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSystem.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DoActionAfterSomeSeconds : Task
    {
        public Action[] _action;
        private float _seconds;

        public DoActionAfterSomeSeconds(float seconds, string taskDescription,params Action[] action) : base(taskDescription)
        {
            this._action = action;
            this._seconds = seconds;
        }

        

        public override async Task<float> TaskAction(CancellationToken cancellationToken)
        {

            await System.Threading.Tasks.Task.Delay(0, cancellationToken);
            await System.Threading.Tasks.Task.Delay((int) _seconds * 1000, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            for (int indexOfAction = 0; indexOfAction < _action.Length; indexOfAction++)
            {
                _action[indexOfAction]();
            }
            return 1;
        }
    }
}