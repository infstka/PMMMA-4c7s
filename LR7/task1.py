import cv2

# Создаем объект для захвата видеопотока с веб-камеры
cap = cv2.VideoCapture(0)  # 0 означает встроенную веб-камеру, можно изменить на путь к видеофайлу

# Инициализируем метод вычитания фона
bg_subtractor = cv2.createBackgroundSubtractorMOG2()

while True:
    ret, frame = cap.read()
    if not ret:
        break

    # Применяем метод вычитания фона
    fg_mask = bg_subtractor.apply(frame)

    # Производим бинаризацию для получения белых объектов на черном фоне
    _, binary_mask = cv2.threshold(fg_mask, 128, 255, cv2.THRESH_BINARY)

    # Находим контуры объектов
    contours, _ = cv2.findContours(binary_mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    for contour in contours:
        if cv2.contourArea(contour) > 1000:  # Минимальная площадь для детектирования движения
            # Выполняем необходимые действия при детектировании движения, например, вывод сообщения или активация сигнала
            print("Движение обнаружено!\n")
    cv2.imshow("LR7 - task1", frame)
#    cv2.imshow("LR7 - task1", binary_mask)

    if cv2.waitKey(1) & 0xFF == 27:
        break

cap.release()
cv2.destroyAllWindows()
