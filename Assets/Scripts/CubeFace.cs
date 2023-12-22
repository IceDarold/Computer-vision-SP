using FaceMovement.Data;
using FaceMovement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Sockets;

public class CubeFace : MonoBehaviour
{
    private List<GameObject> bones;
    private int object_count = 479;
    [SerializeField] private PrimitiveType objects_type = PrimitiveType.Sphere;
    [SerializeField] private DataTransfer dataTransfer;
    void Start()
    {
        bones = new List<GameObject>();
        for (int i = 0; i < object_count; i++)
        {
            bones.Add(GameObject.CreatePrimitive(objects_type));
            Destroy(bones[bones.Count - 1].GetComponent<SphereCollider>());
        }
        try
        {
            FaceDataReceiver.MakeConnectionWithServer();
        }
        catch (SocketException)
        {
            SceneManager.LoadScene("MainMenu");
            dataTransfer.error = true;
        }
    }
    void Update()
    {
        var data = FaceDataReceiver.GetData();
        if (data != null)
        {
            if (dataTransfer.error)
            {
                dataTransfer.error = false;
            }
            var bonsesCoordinations = FaceDataReceiver.SeparateDataToCoodinations(data);
            for (int i = 0; i < bonsesCoordinations.Count; i++)
            {
                var coordinations = bonsesCoordinations[i];
                bones[i].transform.position = new Vector3(Convert.ToSingle(coordinations.Item1) * 100, 
                    (1 - Convert.ToSingle(coordinations.Item2)) * 100, Convert.ToSingle(coordinations.Item3) * 100);
            }
        }
    }
}
