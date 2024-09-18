using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace TaskSystem.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public class AsyncDownloadFile<T> : Task
    {
        private string downloadUrl;
        public T t;
 
        public AsyncDownloadFile(string downloadUrl,string taskDescription) : base(taskDescription) 
        {
            this.downloadUrl = downloadUrl;
        }
        
        public override async Task<float> TaskAction(CancellationToken token)
        {
            using (UnityWebRequest www = new UnityWebRequest(downloadUrl, UnityWebRequest.kHttpVerbGET))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
                UnityWebRequestAsyncOperation operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    Debug.Log("Download Percentage: " + operation.progress);
                    if (token.IsCancellationRequested)
                    {
                        www.Dispose();
                        token.ThrowIfCancellationRequested();
                    }
                    await System.Threading.Tasks.Task.Delay(1000);
                }

                if (www.result == UnityWebRequest.Result.Success)
                {
                    byte[] data = www.downloadHandler.data;

                
                    using (MemoryStream memoryStream = new MemoryStream(data))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        t = (T)bf.Deserialize(memoryStream);
                    }

                    Debug.Log("File successfully downloaded.");
                    Debug.Log("Downloaded data length: " + data.Length);

                    // Debug.Log("WorldSaveData level: " + worldSaveData.level);
                    // Debug.Log("WorldSaveData playerName: " + worldSaveData.playerName);
                    // Debug.Log("WorldSaveData playerPosition: " + worldSaveData.playerPosition);
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