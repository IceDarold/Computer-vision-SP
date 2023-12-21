using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using FaceMovement.Data;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace FaceMovement
{
    public class FaceDataReceiver
    {
        static private TcpClient _client;
        static private NetworkStream _stream;
        static public void MakeConnectionWithServer()
        {
            // Устанавливаем IP-адрес и порт сервера
            string ipAddress = "Storm";
            int port = 8080;

            // Создаем объект TcpClient для подключения к серверу
            _client = new TcpClient(ipAddress, port);
            Debug.Log($"Подключено к серверу {ipAddress}:{port}");

            // Получаем поток для чтения и записи данных
            _stream = _client.GetStream();

        }
        static public List<string> GetData()
        {
            // Чтение ответа от сервера
            byte[] data = new byte[1024 * 32];
            int bytesRead = _stream.Read(data, 0, data.Length);
            string responseMessage = Encoding.UTF8.GetString(data, 0, bytesRead);
            Debug.Log($"Получен ответ от сервера");
            Debug.Log($"length: {responseMessage.Length}");
            List<string> result = responseMessage.Split('_').ToList();
            Debug.Log(result.Count);
            // Закрытие соединения с сервером
            return result;

        }
        static public void CloseConnection()
        {
            _client.Close();
        }
        static public Dictionary<Bone, (double, double, double)> GetBonesOffset(Dictionary<(int, int), Bone> faceForm, List<string> data)
        {
            Dictionary<Bone, (double, double, double)> offsets = new Dictionary<Bone, (double, double, double)>();
            foreach (var connection in faceForm.Keys)
            {
                Debug.Log(data.Count);
                string el = data[connection.Item1];
                var values = el.Split(",");
                var x = ParseCoordinate(values[0]);
                var y = ParseCoordinate(values[1]);
                var z = ParseCoordinate(values[2]);
                offsets[faceForm[connection]] = (x, y, z);
            }
            return offsets;
        }

        static double ParseCoordinate(string coordinate)
        {
            var splitted = coordinate.Split(".");
            double result;

            if (splitted.Length == 3)
            {
                result = double.Parse($"0.{splitted[0]}.{splitted[1]}.{splitted[2]}");
            }
            else
            {
                result = double.Parse(coordinate);
            }

            return result;
        }
    }
}