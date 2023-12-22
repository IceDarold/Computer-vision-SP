using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LaunchCV : MonoBehaviour
{
    private (List<string> outputData, List<string> errors) RunCommand(string fileName, string arguments)
    {
        List<string> errors = new List<string>();
        List<string> outputData = new List<string>();
        // Создаем новый процесс
        Process process = new Process();

        // Задаем параметры для процесса
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = $"{arguments}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.StartInfo = startInfo;

        // Обработчик события для вывода результатов работы Python скрипта
        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                UnityEngine.Debug.Log($"Output: {e.Data}");
                outputData.Add(e.Data);
            }
        };

        // Обработчик события для вывода ошибок Python скрипта
        process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                UnityEngine.Debug.Log($"Error: {e.Data}");
                errors.Add(e.Data);
            }
        };

        // Запускаем процесс
        process.Start();

        // Настраиваем асинхронное чтение вывода
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        // Ждем завершения выполнения процесса
        process.WaitForExit();

        // Закрываем процесс
        process.Close();
        UnityEngine.Debug.Log("Finished");
        return (outputData, errors);
    }
    // Start is called before the first frame update
    void Start()
    {
        //// Относительный путь к интерпретатору Python
        //string pythonPath = @"Assets\Python\python.exe";

        //// Относительный путь к вашему скрипту Python
        //string arguments = "";
        //string scriptPath = @"Assets\Res.py";
        ////UnityEngine.Debug.Log(Environment.CurrentDirectory);
        ////Process process = new Process();
        ////ProcessStartInfo startInfo = new ProcessStartInfo
        ////{
        ////    FileName = pythonPath,
        ////    UseShellExecute = true
        ////};
        ////process.StartInfo = startInfo;

        ////// Запуск процесса
        ////process.Start();
        ////Создание процесса для выполнения скрипта Python
        //Process process = new Process();
        //ProcessStartInfo startInfo = new ProcessStartInfo
        //{
        //    FileName = pythonPath,
        //    RedirectStandardOutput = true,
        //    Arguments = $"\"{scriptPath}\" {arguments}",
        //    UseShellExecute = false,
        //    CreateNoWindow = true
        //};
        //process.StartInfo = startInfo;

        //// Запуск процесса и ожидание завершения выполнения
        //process.Start();
        //process.WaitForExit();

        //// Получение вывода скрипта (если необходимо)
        //string output = process.StandardOutput.ReadToEnd();
        //UnityEngine.Debug.Log($"Output: {output}");

        //// Завершение процесса
        //process.Close();

        //RunCommand(@"Assets\Python\python.exe", "-m pip install --upgrade pip && pip install mediapipe");
        RunCommand(@"Assets\Python\python.exe", "python --version");
        //(List<string> outputData, List<string> errors) = RunCommand(@"Assets\Python\python.exe", @"Assets\Res.py");
        //if (errors.Contains("Error: ModuleNotFoundError: No module named \'mediapipe\'"))
        //{
        //    RunCommand(@"Assets\Python\python.exe", "$\"-m pip install --upgrade pip && pip install mediapipe");
        //    UnityEngine.Debug.Log("Instal mediapipe...");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
