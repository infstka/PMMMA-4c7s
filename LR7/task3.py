import cv2

cap = cv2.VideoCapture(0)  # 0 для встроенной веб-камеры, можно изменить на путь к видеофайлу

# Инициализируем трекер CSRT
# Реализация основана на дискриминативном корреляционном фильтре с канальной и
# пространственной надежностью.
tracker = cv2.TrackerCSRT_create()

ret, frame = cap.read()
bbox = cv2.selectROI("Select Object to Track", frame)

# Инициализируем трекер с первоначальным положением объекта
tracker.init(frame, bbox)

while True:
    ret, frame = cap.read()
    if not ret:
        break

    # Обновляем трекер с текущим кадром
    success, bbox = tracker.update(frame)

    if success:
        # Рисуем прямоугольник вокруг отслеживаемого объекта
        x, y, w, h = [int(i) for i in bbox]
        cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)

    cv2.imshow("LR7 - task3", frame)

    if cv2.waitKey(1) & 0xFF == 27:
        break

cap.release()
cv2.destroyAllWindows()
