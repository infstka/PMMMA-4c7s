import cv2

# Загрузка классификаторов Хаара для лица, глаз и рта
face_cascade = cv2.CascadeClassifier('haarcascade/haarcascade_frontalface_default.xml')
eye_cascade = cv2.CascadeClassifier('haarcascade/haarcascade_eye.xml')
mouth_cascade = cv2.CascadeClassifier('haarcascade/haarcascade_mouth.xml')

# Функция для обнаружения и рисования рамок вокруг лица, глаз и рта, а также проверки наличия маски
def detect_face_mask(image):
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    
    # Обнаружение лица
    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.3, minNeighbors=5, minSize=(30, 30))
    
    for (x, y, w, h) in faces:
        cv2.rectangle(image, (x, y), (x + w, y + h), (0, 255, 0), 2)
        roi_gray = gray[y:y + h, x:x + w]
        roi_color = image[y:y + h, x:x + w]
        
        # Обнаружение рта внутри области лица
        mouths = mouth_cascade.detectMultiScale(roi_gray)
        for (mx, my, mw, mh) in mouths:
            cv2.rectangle(roi_color, (mx, my), (mx + mw, my + mh), (0, 0, 255), 2)
        
        # Проверка наличия маски (если рот обнаружен, то маска, возможно, не надета)
        if len(mouths) == 0:
            cv2.putText(image, "Mask on", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)
        else:
            cv2.putText(image, "Mask off", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
    
    return image

# Использование веб-камеры в качестве источника видео
cap = cv2.VideoCapture(0)  

while True:
    ret, frame = cap.read()
    if not ret:
        break
    
    # Обнаружение лица, глаз, рта и проверка наличия маски на каждом кадре
    result_frame = detect_face_mask(frame)
    
    # Отображение результата
    cv2.imshow('LR6 - task2', result_frame)
    
    # Выход из цикла при нажатии клавиши 'q'
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()
