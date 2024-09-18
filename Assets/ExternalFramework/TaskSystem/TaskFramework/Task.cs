namespace TaskSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using System;

    public class Task
    {
        private string taskDescription;
        protected string cancellationDescription;
        private Task<float> _taskAction;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Destroying created actions and tasks.
        /// </summary>
        ~Task()
        {
            if (_taskAction != null)
            {
                _taskAction.Dispose();
                _cancellationTokenSource.Dispose();
            }
        }

        public Task(string taskDescription)
        {
            this.taskDescription = taskDescription;
        }

        /// <summary>
        /// This function allows you to start the task. 
        /// </summary>
        public virtual Task Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _taskAction = TaskAction(_cancellationTokenSource.Token);
            return this;
        }

        /// <summary>
        /// This function allows you to stop the task
        /// </summary>
        public virtual Task Stop()
        {
            if (_taskAction == null)
            {
                Debug.Log("Task wasn't started : " + taskDescription);
                return this;
            }

            if (_taskAction.IsCompleted)
            {
                try
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                    _taskAction?.Dispose();
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e);
                    // throw;
                }
            }
            else
            {
                WaitForTaskToEnd();
            }

            return this;
        }

        private async void WaitForTaskToEnd()
        {
            _cancellationTokenSource.Cancel();
            try
            {
                await _taskAction;
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                // throw;
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                _taskAction.Dispose();
            }
        }

        /// <summary>
        /// This Function allows you to restart the task.
        /// </summary>
        public virtual Task Restart()
        {
            Stop();
            Start();
            return this;
        }

        /// <summary>
        /// Get Task
        /// </summary>
        /// <returns></returns>
        public Task<float> GetTask()
        {
            return _taskAction;
        }

        public CancellationTokenSource GetTokenSource()
        {
            return _cancellationTokenSource;
        }

        /// <summary>
        /// It will return the value between 0 and 1
        /// </summary>
        /// <returns></returns>
        public virtual float GetPercentage()
        {
            if (_taskAction == null)
            {
                Debug.LogError("Task wasn't created");
                return 0;
            }

            return _taskAction.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Task Action
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<float> TaskAction(CancellationToken cancellationToken)
        {
            await System.Threading.Tasks.Task.Delay(0, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            return 1;
        }

        /// <summary>
        /// Returns true if task is completed. 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTaskCompleted()
        {
            return _taskAction.IsCompleted;
        }

        /// <summary>
        /// Returns true if task is faulted. 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTaskFaulted()
        {
            return _taskAction.IsFaulted;
        }

        /// <summary>
        /// Returns true if task is canceld. 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTaskCanceld()
        {
            return _taskAction.IsCanceled;
        }

        public virtual string GetTaskDescription()
        {
            return taskDescription;
        }


        public string GetCancellationDescription()
        {
            return cancellationDescription;
        }
    }
}