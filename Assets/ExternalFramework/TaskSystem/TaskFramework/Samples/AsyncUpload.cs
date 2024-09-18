using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace TaskSystem.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AsyncUpload : Task
    {
        private string uploadUrl;
        private string localPath;

        public AsyncUpload(string uploadUrl, string localPath,string taskDescription) : base(taskDescription)
        {
            this.uploadUrl = uploadUrl;
            this.localPath = localPath;
        }
        
        public override async Task<float> TaskAction(CancellationToken token)
        {
            using (UnityWebRequest www = new UnityWebRequest(uploadUrl, UnityWebRequest.kHttpVerbPUT))
            {
                www.uploadHandler = new UploadHandlerFile(localPath);
                UnityWebRequestAsyncOperation operation = www.SendWebRequest();
            
            
                while (!operation.isDone)
                {
                    Debug.Log("Download Percentage : " + operation.progress);
                    if (token.IsCancellationRequested)
                    {
                        www.Dispose();
                        token.ThrowIfCancellationRequested();
                    }
            
                    await System.Threading.Tasks.Task.Delay(1000);
                }
            
                if (www.error == null)
                {
                    Debug.Log("File successfully downloaded and saved to" + localPath);
                }
                else
                {
                    Debug.LogError(www.error);
                }
            }
            return 1;
        }
    }
}