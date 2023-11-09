import cv2 
import mediapipe as mp 
import time 
import numpy as np
import math  
import screen_brightness_control as sbc  
from comtypes import CLSCTX_ALL 
from pycaw.pycaw import AudioUtilities, IAudioEndpointVolume 

class MediaPipe:
    # инициализация класса для обнаружения рук с определенными параметрами
    def __init__(self, mode=False, max_hands=2, complex=1, detection_con=0.5, track_con=0.5):
        self.mode = mode  
        self.max_hands = max_hands  
        self.complex = complex  # сложность
        self.detection_con = detection_con  # порог обнаружения
        self.track_con = track_con  # порог отслеживания
        self.mp_hands = mp.solutions.hands  # модель обнаружения рук
        self.hands = self.mp_hands.Hands(self.mode, self.max_hands, self.complex, self.detection_con, self.track_con)  # обнаружение рук
        self.mp_draw = mp.solutions.drawing_utils  # утилиты для рисования ключевых точек

    # функция поиска рук на изображении        
    def find_hands(self, img, draw=True):
        img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)  # конвертация изображения в ргб формат
        self.results = self.hands.process(img_rgb)  # получение результатов обработки рук
        if self.results.multi_hand_landmarks:  # если есть обнаруженные ключевые точки рук
            for hand_landmarks in self.results.multi_hand_landmarks:
                if draw:
                    self.mp_draw.draw_landmarks(img, hand_landmarks, self.mp_hands.HAND_CONNECTIONS)  # рисование точек и соединений между ними
        return img
    
    # функция определения позиции ключевых точек руки на изображении
    def find_position(self, img, hand_no=0, draw=True):
        lm_list = []  # список координат ключевых точек
        if self.results.multi_hand_landmarks:
            my_hand = self.results.multi_hand_landmarks[hand_no]
            for idx, lm in enumerate(my_hand.landmark):
                height, width, _ = img.shape  # высота, ширина изображения
                cx, cy = int(lm.x * width), int(lm.y * height)  # координаты точек на изображении
                lm_list.append([idx, cx, cy])  # добавление индекса и координат в список
                if draw:
                    cv2.circle(img, (cx, cy), 2, (255, 0, 255), cv2.FILLED)  # рисование точек на изображении
        return lm_list

# функция определения жеста кулака
def is_fist(lm_list):
    fingers_status = [lm_list[i][2] < lm_list[i - 2][2] for i in range(5, 21, 4)]  # проверка положения пальцев
    if all(fingers_status):
        return True
    return False

def main():
    cap = cv2.VideoCapture(0)  
    w_cam, h_cam = 640, 480  
    cap.set(3, w_cam) 
    cap.set(4, h_cam) 

    p_time = 0 
    detector = MediaPipe(detection_con=0.8)  # инициализация детектора рук с заданным порогом

    devices = AudioUtilities.GetSpeakers() 
    interface = devices.Activate(IAudioEndpointVolume._iid_, CLSCTX_ALL, None) 
    volume = interface.QueryInterface(IAudioEndpointVolume)  # интерфейс для управления громкостью
    vol_range = volume.GetVolumeRange()  # диапазон громкости
    min_vol = vol_range[0]  
    max_vol = vol_range[1]  
    select_mode = "Volume"  # по умолчанию управление громкостью

    while True:
        ret, img = cap.read()  # чтение кадра из видеопотока
        img = detector.find_hands(img)  # поиск рук на изображении
        lm_list = detector.find_position(img, draw=False)  # определение позиции ключевых точек, без рисования

        if lm_list:
            x1, y1 = lm_list[4][1], lm_list[4][2]  # координаты указательного пальца
            x2, y2 = lm_list[8][1], lm_list[8][2]  # координаты большого пальца
            cx, cy = (x1 + x2) // 2, (y1 + y2) // 2  # координаты середины между указательным и большим пальцами

            cv2.circle(img, (x1, y1), 15, (255, 0, 255), cv2.FILLED)  # круг вокруг указательного пальца
            cv2.circle(img, (x2, y2), 15, (255, 0, 255), cv2.FILLED)  # круг вокруг большого пальца
            cv2.line(img, (x1, y1), (x2, y2), (255, 255, 255), 3)  # рисование линии между пальцами

            length = math.hypot(x2 - x1, y2 - y1)  # длина между пальцами

            if select_mode == "Volume":
                vol = np.interp(length, [35, 100], [min_vol, max_vol])  # интерполяция значений для громкости
                volume.SetMasterVolumeLevel(vol, None)  # установка уровня громкости
            elif select_mode == "Brightness":
                bri = np.interp(length, [35, 100], [0, 100])  # интерполяция значений для яркости
                sbc.set_brightness(bri)  # установка яркости
            else:
                print("Unknown")  

            if is_fist(lm_list):  # если обнаружен кулак
                select_mode = "Brightness" if select_mode == "Volume" else "Volume"  # переключение режима

        cv2.putText(img, f"Mode: {select_mode}", (10, 70), cv2.FONT_HERSHEY_SIMPLEX, 1, (255, 255, 255), 2)  # вывод режима на изображении

        cv2.imshow("LR8 - MediaPipe", img)  

        key = cv2.waitKey(1) & 0xFF  
        if key == ord('q'):  
            break  

    cap.release() 
    cv2.destroyAllWindows() 

if __name__ == "__main__":
    main() 
