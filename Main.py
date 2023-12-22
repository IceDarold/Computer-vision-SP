# STEP 1: Import the necessary modules.
import socket

import mediapipe as mp
import numpy as np
from mediapipe.tasks import python
from mediapipe.tasks.python import vision
import cv2
from VisualizationUtilities import *

def start_get_data():
    is_delta_data = False
    is_show_im = True
    show_landmarks = False
    delta_data = ""
    base_options = python.BaseOptions(model_asset_path='face_landmarker.task')
    options = vision.FaceLandmarkerOptions(base_options=base_options,
                                           output_face_blendshapes=True,
                                           output_facial_transformation_matrixes=True,
                                           num_faces=1)
    detector = vision.FaceLandmarker.create_from_options(options)
    cap = cv2.VideoCapture(0)

    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    hostname = socket.gethostname()
    server.bind((hostname, 8080))
    server.listen(5)

    print("Server starts")

    con, addr = server.accept()

    print(f"Connection: {con}\nAddress: {addr}")
    data = [[0, 0, 0] for _ in range(478)]
    while cap.isOpened():
        ret, image = cap.read()
        image = np.fliplr(image)
        cv2.imwrite("image.png", image)
        if cv2.waitKey(1) & 0xFF == ord('q') or not ret:
            break
        image = mp.Image.create_from_file("image.png")
        detection_result = detector.detect(image)
        with open("D:/data_face.txt", "w") as f:
            f.write("")

        if len(detection_result.face_landmarks) > 0:
            res_data = ""
            for i in range(len(detection_result.face_landmarks[0])):
                if is_delta_data:
                    s = (f"{data[i][0] - detection_result.face_landmarks[0][i].x},"
                         f"{data[i][1] - detection_result.face_landmarks[0][i].y},"
                         f"{data[i][2] - detection_result.face_landmarks[0][i].z}_")
                    print(s)
                    res_data += s
                    data[i][0] = detection_result.face_landmarks[0][i].x
                    data[i][1] = detection_result.face_landmarks[0][i].y
                    data[i][2] = detection_result.face_landmarks[0][i].z
                else:
                    s = (f"{detection_result.face_landmarks[0][i].x}, "
                                   f"{detection_result.face_landmarks[0][i].y}, "
                                   f"{detection_result.face_landmarks[0][i].z}_")
                    print(s)
                    res_data += s
            message = res_data
            print(len(message.split("_")))
            con.send(message.encode())

        if is_show_im:
            if show_landmarks:
                annotated_image = draw_landmarks_on_image(image.numpy_view(), detection_result)
                cv2.imshow("im", cv2.cvtColor(annotated_image, cv2.COLOR_RGB2BGR))
            else:
                cv2.imshow("im", cv2.cvtColor(image.numpy_view(), cv2.COLOR_RGB2BGR))

if __name__ == "__main__":
    start_get_data()