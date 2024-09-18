using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace TaskSystem.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AsyncDownloadAndSaveFile : Task
    {
        private string downloadUrl;
        private string path;
 
        public AsyncDownloadAndSaveFile(string downloadUrl, string path,string taskDescription) : base(taskDescription) 
        {
            this.downloadUrl = downloadUrl;
            this.path = path;
        }
        
        public override async Task<float> TaskAction(CancellationToken token)
        {
            UnityWebRequest www = new UnityWebRequest(downloadUrl,UnityWebRequest.kHttpVerbGET);
            www.downloadHandler = new DownloadHandlerFile(path);
            UnityWebRequestAsyncOperation operation = www.SendWebRequest();
            
            
            while (!operation.isDone)
            {
                Debug.Log("Download Percentage : "+operation.progress);
                if (token.IsCancellationRequested)
                {
                    www.Dispose();
                    token.ThrowIfCancellationRequested();
                }
                await System.Threading.Tasks.Task.Delay(1000);
            }
            
            if (www.error == null)
            {
                Debug.Log("File successfully downloaded and saved to" + path);
            }
            else
            {
                Debug.LogError(www.error);
            }
            www.Dispose();
            return 1;
        }
    }
}