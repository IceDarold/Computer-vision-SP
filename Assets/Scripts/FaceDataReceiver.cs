using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using FaceMovement.Data;

namespace FaceMovement
{
    public class FaceDataReceiver
    {
        static public List<string> GetData()
        {
            string path = "D:/data_face.txt";
            List<string> data = new List<string>();
            try
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    data.Add(line);
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reading file error: {ex.Message}");
            }
            return null;
        }

        static public Dictionary<Bone, (double, double, double)> GetBonesOffset(Dictionary<(int, int), Bone> faceForm, List<string> data)
        {
            Dictionary<Bone, (double, double, double)> offsets = new Dictionary<Bone, (double, double, double)>();
            foreach (var connection in faceForm.Keys)
            {
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