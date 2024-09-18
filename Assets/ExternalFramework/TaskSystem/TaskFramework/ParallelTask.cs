namespace TaskSystem
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class ParallelTask : Task
    {
        private Task[] _frameworkTasks;
        private List<Task<float>> tasks;
        public Action onTaskStarted = delegate { };
        public Action onTaskCompleted = delegate { };
        public Action<float, string> onTaskPercentageUpdated = delegate(float f, string description) { };
        public Action<string> onTaskCancelled = delegate(string s) { };

        public ParallelTask(string taskDescription, params Task[] frameworkTasks) : base(taskDescription)
        {
            this._frameworkTasks = frameworkTasks;
            tasks = new List<Task<float>>();
        }

        public override Task Start()
        {
            for (int indexOfTask = 0; indexOfTask < _frameworkTasks.Length; indexOfTask++)
            {
                _frameworkTasks[indexOfTask].Start();
                tasks.Add(_frameworkTasks[indexOfTask].GetTask());
            }

            base.Start();
            return this;
        }

        public override Task Stop()
        {
            base.Stop();
            for (int indexOfTask = 0; indexOfTask < _frameworkTasks.Length; indexOfTask++)
            {
                _frameworkTasks[indexOfTask].Stop();
            }

            return this;
        }

        public override async Task<float> TaskAction(CancellationToken cancellationToken)
        {
            onTaskStarted();
            await System.Threading.Tasks.Task.Delay(0, cancellationToken);
            onTaskPercentageUpdated(0f, _frameworkTasks[0].GetTaskDescription());
            bool foundCancelledTask = false;
            Task cancelledTask = null;
            while (!System.Threading.Tasks.Task.WhenAll(tasks).IsCompleted)
            {
                int completedTaskCount = 0;
                for (int indexOfTask = 0; indexOfTask < tasks.Count; indexOfTask++)
                {
                    if (tasks[indexOfTask].IsCanceled || tasks[indexOfTask].IsFaulted)
                    {
                        cancelledTask = _frameworkTasks[indexOfTask];
                        foundCancelledTask = true;
                    }
                    else if(tasks[indexOfTask].IsCompleted)
                    {
                        completedTaskCount++;
                    }
                }

                onTaskPercentageUpdated((((float) completedTaskCount) / ((float) tasks.Count)), GetTaskDescription());
                if (foundCancelledTask)
                {
                    break;
                }

                await System.Threading.Tasks.Task.Delay(200);
            }

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