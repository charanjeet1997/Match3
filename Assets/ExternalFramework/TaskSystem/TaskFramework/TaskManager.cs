using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TaskManager : MonoBehaviour
    {
        #region PUBLIC_VARS

        public static TaskManager Instance;
        #endregion
        
        #region PRIVATE_VARS
        private Dictionary<string, Task> _tasks;
        private CancellationTokenSource _cancellationTokenSource;
        private System.Threading.Tasks.Task executionTask;
        #endregion

        #region UNITY_CALLBACKS
        private void Awake()
        {
            if(TaskManager.Instance!=null)
                Destroy(gameObject);
            
            if (TaskManager.Instance == null)
                Instance = this;
        }

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _tasks = new Dictionary<string, Task>();
            executionTask = Execute(_cancellationTokenSource.Token);
        }

        private void OnDestroy()
        {
            StopAllRunningTask();
            StopExecution();
            
        }

        #endregion

        #region PUBLIC_METHODS
        public void AddTask(string id,Task task)
        {
            if (_tasks.Keys.Contains(id))
            {
                Debug.Log($"Task with this id : {id} is already exist!!!.");
                return;
            }
            task.Start();
            _tasks.Add(id,task);
        }

        public void RemoveTask(string id)
        {
            if (!_tasks.Keys.Contains(id))
            {
                Debug.Log($"Task with this id : {id} is not exist!!!.");
                return;
            }

            Task task = _tasks[id];
            _tasks.Remove(id);
            task.Stop().Dispose();
        }
        
        public void RestartTask(string id)
        {
            if (!_tasks.Keys.Contains(id))
            {
                Debug.Log($"Task with this id : {id} is not exist!!!.");
                return;
            }
            Task task = _tasks[id];
            task.Stop();
            _tasks.Remove(id);
            AddTask(id,task);
        }

        public Task[] GetAllRunningTask()
        {
            return _tasks.Values.ToArray();
        }

        public bool IsTaskExist(string id)
        {
            return _tasks.Keys.Contains(id);
        }

        public bool IsTaskCompleted(string id)
        {
            return _tasks[id].IsTaskCompleted();
        }

        public void StopAllRunningTask()
        {
            Task[] runningTask = _tasks.Values.ToArray();
            for (int indexOfTask = 0; indexOfTask < runningTask.Length; indexOfTask++)
            {
                runningTask[indexOfTask].Stop().Dispose();
            }
            _tasks.Clear();
        }
        #endregion

        #region PRIVATE_METHODS
        private async System.Threading.Tasks.Task Execute(CancellationToken cancellationToken)
        {
            await System.Threading.Tasks.Task.Yield();
            while (gameObject.activeInHierarchy)
            {
                // Debug.Log("Task system Running");
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
                RemoveCompletedTask(AddTaskToRemoveListIfTaskIsCompletedFaultedOrCanceld());
                await System.Threading.Tasks.Task.Yield();
            }
        }

        private async void StopExecution()
        {
            _cancellationTokenSource.Cancel();
            try
            {
                await executionTask;
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                // throw;
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                executionTask.Dispose();
            }
        }
        private void RemoveCompletedTask(List<string> completedTaskKeys)
        {
            for (int indexOfCompletedTask = 0; indexOfCompletedTask < completedTaskKeys.Count; indexOfCompletedTask++)
            {    
                RemoveTask(completedTaskKeys[indexOfCompletedTask]);
            }
        }
        /// <summary>
        /// Created this way for keeping the performance. 
        /// </summary>
        /// <returns></returns>
        private List<string> AddTaskToRemoveListIfTaskIsCompletedFaultedOrCanceld()
        {
            List<string> completedTaskKeys = new List<string>();
            for (int indexOfTask = 0; indexOfTask < _tasks.Count; indexOfTask++)
            {
                var item = _tasks.ElementAt(indexOfTask);
                if (item.Value.IsTaskCompleted()||item.Value.IsTaskFaulted()||item.Value.IsTaskCanceld())
                {
                    completedTaskKeys.Add(item.Key);
                }
            }
            return completedTaskKeys;
        }
        #endregion
    }
}