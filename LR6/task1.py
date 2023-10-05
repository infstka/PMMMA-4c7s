import cv2

# Загрузка классификаторов Хаара для лица, глаз и рта
face_cascade = cv2.CascadeClassifier('haarcascade/haarcascade_frontalface_default.xml')
eye_cascade = cv2.CascadeClassifier('haarcascade/haarcascade_eye.xml')
mouth_cascade = cv2.CascadeClassifier('haarcascade/haarcascade_mouth.xml')

# Функция для обнаружения и рисования рамок вокруг лица, глаз и рта
def detect_face_eyes_mouth(image):
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    
    # Обнаружение лица
    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.3, minNeighbors=5, minSize=(30, 30))
    
    for (x, y, w, h) in faces:
        cv2.rectangle(image, (x, y), (x + w, y + h), (0, 255, 0), 2)
        roi_gray = gray[y:y + h, x:x + w]
        roi_color = image[y:y + h, x:x + w]
        
        # Обнаружение глаз внутри области лица
        eyes = eye_cascade.detectMultiScale(roi_gray)
        for (ex, ey, ew, eh) in eyes:
            cv2.rectangle(roi_color, (ex, ey), (ex + ew, ey + eh), (255, 0, 0), 2)
        
        # Обнаружение рта внутри области лица
        mouths = mouth_cascade.detectMultiScale(roi_gray)
        for (mx, my, mw, mh) in mouths:
            cv2.rectangle(roi_color, (mx, my), (mx + mw, my + mh), (0, 0, 255), 2)
    
    return image

# Использование веб-камеры в качестве источника видео
cap = cv2.VideoCapture(0)  
while True:
    ret, frame = cap.read()
    if not ret:
        break
    
    # Обнаружение лица, глаз и рта на каждом кадре
    result_frame = detect_face_eyes_mouth(frame)
    
    # Отображение результата
    cv2.imshow('LR6 - task1', result_frame)
    
    # Выход из цикла при нажатии клавиши 'q'
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()
