using System;
using UnityEngine;

namespace TaskSystem
{
    using System.Threading;
    using System.Threading.Tasks;

    public class SequencialTask : Task
    {
        public Task[] _frameworkTasks;
        public Action onTaskStarted = delegate { };
        public Action onTaskCompleted = delegate { };
        public Action<string> onTaskCancelled = delegate(string s) { };

        public Action<float, string> onTaskPercentageUpdated = delegate(float f, string description) { };


        public SequencialTask(string taskDescription, params Task[] frameworkTasks) : base(taskDescription)
        {
            this._frameworkTasks = frameworkTasks;
        }

        public override Task Stop()
        {
            for (int indexOfTask = 0; indexOfTask < _frameworkTasks.Length; indexOfTask++)
            {
                _frameworkTasks[indexOfTask].Stop();
            }

            base.Stop();
            return this;
        }

        public override async Task<float> TaskAction(CancellationToken cancellationToken)
        {
            onTaskStarted();
            await System.Threading.Tasks.Task.Delay(0, cancellationToken);

            Task cancelledTask = null;
            bool foundCancelledTask = false;

            for (int indexOfTask = 0; indexOfTask < _frameworkTasks.Length; indexOfTask++)
            {
                _frameworkTasks[indexOfTask].Start();
                onTaskPercentageUpdated((((float) indexOfTask) / ((float) _frameworkTasks.Length)),
                    _frameworkTasks[indexOfTask].GetTaskDescription());

                while (!_frameworkTasks[indexOfTask].GetTask().IsCompleted && !_frameworkTasks[indexOfTask].GetTask().IsFaulted && !_frameworkTasks[indexOfTask].GetTask().IsCanceled)
                {
                    await System.Threading.Tasks.Task.Delay(200);
                }

                if (_frameworkTasks[indexOfTask].GetTask().IsCanceled ||
                    _frameworkTasks[indexOfTask].GetTask().IsFaulted)
                {
                    cancelledTask = _frameworkTasks[indexOfTask];
                    foundCancelledTask = true;
                    break;
                }
                
                if (foundCancelledTask)
                {
                    break;
                }
            }
            await System.Threading.Tasks.Task.Delay(200);

            if (foundCancelledTask)
            {
                Stop();
                onTaskCancelled(cancelledTask.GetCancellationDescription());
            }
            else
            {
                onTaskCompleted();
            }

            return 1f;
        }
    }
}