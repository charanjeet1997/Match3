// using System;
// using System.IO;
// using System.Threading;
// using System.Threading.Tasks;
// // using BestHTTP;
// using TaskSystem.Examples;
// using UnityEngine.Networking;
// using UnityEngine.SceneManagement;
//
// namespace TaskSystem
// {
//     using System.Collections;
//     using System.Collections.Generic;
//     using UnityEngine;
//
//     public class TaskSystemTestCases : MonoBehaviour
//     {
//         public string url;
//         public string bigDataDownloadUrl;
//
//         public void Start()
//         {
//             // TaskManager.Instance.AddTask("WaitTask",new DoActionAfterSomeSeconds(4f,()=>PrintMessage("Called From Task MAnager")));
//             // TaskManager.Instance.AddTask("WaitTask",new AsyncSceneload("ARInfoMainScene",LoadSceneMode.Additive,()=>PrintMessage("SceneLoadStarted"),()=>PrintMessage("SceneLoadCompleted")));
//
//             // TaskManager.Instance.AddTask("WaitTask", new SequencialFrameworkTask(
//             //     new DoActionAfterSomeSeconds(2f, () => PrintMessage("First Sequencial Task")),
//             //     new ParallelFrameworkTask(
//             //         new DoActionAfterSomeSeconds(2f, () => PrintMessage("First Parallel Task")),
//             //         new DoActionAfterSomeSeconds(2f, () => PrintMessage("Second Parallel Task")))
//             // ));
//
//             // TaskManager.Instance.AddTask("WaitTask",   
//             //     new ParallelFrameworkTask( 
//             //         new DoActionAfterSomeSeconds(2f,()=>PrintMessage("First Parallel Task")),
//             //         new DoActionAfterSomeSeconds(2f,()=>PrintMessage("Second Parallel Task")))
//             // );
//             // new SequencialTask(
//             //     "Sequencial Task",new DoActionAfterSomeSeconds(2f, "Printing debug task",() => PrintMessage("Third Sequencial Task")),
//             //     new DoActionAfterSomeSeconds(2f, "Printing debug task",() => PrintMessage("Fourth Sequencial Task"))
//             // )
//             var t=new SequencialTask("Sequencial Task",
//                 new DoActionAfterSomeSeconds(1f, "Printing debug task", () => PrintMessage("First Sequencial Task")),
//                 new DoActionAfterSomeSeconds(2f, "Printing debug task", () => PrintMessage("Second Sequencial Task")),
//                 new CancelTask(2f,"CancelledTask",()=>PrintMessage("Third Cancel Task")),
//                 new DoActionAfterSomeSeconds(4f, "Printing debug task", () => PrintMessage("Fourth Sequencial Task")),
//             new DoActionAfterSomeSeconds(5f, "Printing debug task", () => PrintMessage("Fifth Sequencial Task"))
//
//
//             );
//             // var t = new ParallelTask("Parallel task",
//             //     new DoActionAfterSomeSeconds(1f, "Printing debug task",() => PrintMessage("First Sequencial Task")),
//             //     new DoActionAfterSomeSeconds(2f, "Printing debug task",() => PrintMessage("First Sequencial Task")),
//             //     new DoActionAfterSomeSeconds(3f, "Printing debug task",() => PrintMessage("First Sequencial Task")),
//             //     
//             //     new DoActionAfterSomeSeconds(4f, "Printing debug task",() => PrintMessage("First Sequencial Task")),
//             //     new CancelTask(3f,"CancelledTask",()=>PrintMessage("Third Cancel Task"))
//             //     
//             // );
//             
//             
//             t.onTaskStarted += OnTaskStarted;
//             t.onTaskCompleted += OnTaskCompleted;
//             t.onTaskPercentageUpdated += OnTaskRunning;
//             t.onTaskCancelled += OnTaskCancelled;
//             
//             TaskManager.Instance.AddTask("WaitTask", t);
//         }
//
//         public void OnTaskStarted()
//         {
//             Debug.Log("OnTaskStarted");
//         }
//
//         public void OnTaskCompleted()
//         {
//             Debug.Log("OnTaskCompleted");
//
//         }
//
//         public void OnTaskRunning(float percentage,string description)
//         {
//             Debug.Log("OnTaskRunning : " +percentage );
//             Debug.Log("Description : " + description);
//         }
//
//         public void OnTaskCancelled(string cancellationMessage)
//         {
//             Debug.Log("Cancellation Task Message : "+cancellationMessage);
//             Debug.Log("Contain Task : "+TaskManager.Instance.IsTaskExist("WaitTask"));
//         }
//
//         public void PrintMessage(string message)
//         {
//             Debug.Log(message);
//         }
//
//         #region NormalRequestResponse
//
//         [ContextMenu("CallAPI")]
//         private void CallAPI()
//         {
//             // StartCoroutine(GetService(url));
//             GetAsyncService(url);
//         }
//
//         public async System.Threading.Tasks.Task GetAsyncService(string url)
//         {
//             var request = new HTTPRequest(new Uri(url), HTTPMethods.Get);
//
//             CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
//             try
//             {
//                 // Debug.Log(await request.GetAsStringAsync(tokenSource.Token));
//                 HTTPResponse response = await request.GetHTTPResponseAsync(tokenSource.Token);
//                 Debug.Log("Status code : " + response.DataAsText.ToString());
//             }
//             catch (Exception ex)
//             {
//                 Debug.LogException(ex);
//             }
//
//             // don't forget to dispose! ;)
//             tokenSource.Dispose();
//         }
//
//         #endregion
//
//
//         #region BigDataUpload
//
//         #endregion
//
//         #region BigDataDownload
//
//         [ContextMenu("CallDownload")]
//         public void CallDownload()
//         {
//             // CancellationTokenSource tokenSource = new CancellationTokenSource();
//             // DownloadService(bigDataDownloadUrl,tokenSource.Token);
//             Debug.Log(Path.Combine(Application.persistentDataPath, "test.mp4"));
//             // TaskManager.Instance.AddTask(
//             //     "DownloadTask",new AsyncDownloadAndSaveFile(
//             //         bigDataDownloadUrl,
//             //         Path.Combine(Application.persistentDataPath, "test.mp4")
//             //     ));
//         }
//         [ContextMenu("CancelDownload")]        
//         public void CancelDownload()
//         {
//             TaskManager.Instance.RemoveTask("DownloadTask");
//         }
//
//         public async System.Threading.Tasks.Task DownloadService(string url, CancellationToken token)
//         {
//             UnityWebRequest www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
//             string path = Path.Combine(Application.persistentDataPath, "unity3d.mp4");
//             www.downloadHandler = new DownloadHandlerFile(path);
//             UnityWebRequestAsyncOperation operation = www.SendWebRequest();
//
//             while (!operation.isDone)
//             {
//                 await System.Threading.Tasks.Task.Delay(100);
//             }
//
//             if (www.error == null)
//             {
//                 Debug.Log("File successfully downloaded and saved to " + path);
//             }
//             else
//             {
//                 Debug.LogError(www.error);
//             }
//
//             return;
//         }
//
//         #endregion
//
//         #region BIGDataUpload
//
//         public string uploadUrl;
//         public string uploadFilePath;
//         [ContextMenu("CallUpload")]
//         public void CallUpload()
//         {
//             TaskManager.Instance.AddTask("UploadTask",new AsyncUpload(uploadUrl,uploadFilePath,"Async upload"));
//         }
//
//         #endregion
//     }
// }