using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TaskSystem.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AsyncSceneload : Task
    {
        private string _scene;
        private LoadSceneMode _loadSceneMode;
        private Action _onSceneLoadStarted;
        private Action _onSceneLoadCompleted;        

        public AsyncSceneload(string scene, string taskDescription,LoadSceneMode loadSceneMode, Action onSceneLoadStarted, Action onSceneLoadCompleted) : base(taskDescription)
        {
            _scene = scene;
            _loadSceneMode = loadSceneMode;
            _onSceneLoadStarted = onSceneLoadStarted;
            _onSceneLoadCompleted = onSceneLoadCompleted;
        }
        
        public override async Task<float> TaskAction(CancellationToken cancellationToken)
        {
            await System.Threading.Tasks.Task.Delay(0, cancellationToken);
            _onSceneLoadStarted();
            AsyncOperation sceneLoadAsync =  SceneManager.LoadSceneAsync(_scene, _loadSceneMode);
            while (!sceneLoadAsync.isDone)
            {
                Debug.Log("Progress : "+sceneLoadAsync.progress);
                await System.Threading.Tasks.Task.Yield();
            }
            _onSceneLoadCompleted();
            return 1;
        }
    }
}