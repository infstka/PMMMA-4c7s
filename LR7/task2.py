import cv2
import numpy as np

# Открываем веб-камеру
cap = cv2.VideoCapture(0)

# Инициализируем объект для вычисления оптического потока
lk_params = dict(winSize=(15, 15), maxLevel=2, criteria=(cv2.TERM_CRITERIA_EPS | cv2.TERM_CRITERIA_COUNT, 10, 0.03))

# Чтобы узнать первый кадр
ret, first_frame = cap.read()
prev_gray = cv2.cvtColor(first_frame, cv2.COLOR_BGR2GRAY)

# Инициализируем начальные точки для отслеживания (можно задать вручную)
# В этом примере мы просто выбираем некоторые точки на первом кадре
prev_points = cv2.goodFeaturesToTrack(prev_gray, maxCorners=100, qualityLevel=0.3, minDistance=7, blockSize=7)

while True:
    # Захватываем текущий кадр
    ret, frame = cap.read()
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    # Вычисляем оптический поток с помощью метода Лукаса-Канаде
    next_points, status, error = cv2.calcOpticalFlowPyrLK(prev_gray, gray, prev_points, None, **lk_params)

    # Отфильтровываем только те точки, для которых оптический поток найден
    good_new = next_points[status == 1]
    good_old = prev_points[status == 1]

    # Рисуем оптический поток на кадре
    for i, (new, old) in enumerate(zip(good_new, good_old)):
        a, b = new.ravel()
        c, d = old.ravel()
        mask = cv2.line(frame, (int(a), int(b)), (int(c), int(d)), (0, 0, 255), 2)
        frame = cv2.circle(frame, (int(a), int(b)), 5, (0, 0, 255), -1)

    # Отображаем кадр с оптическим потоком
    cv2.imshow("LR7 - task2", frame)

    # Для выхода нажмите 'q'
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

    # Обновляем предыдущий кадр и точки
    prev_gray = gray.copy()
    prev_points = good_new.reshape(-1, 1, 2)

# Освобождаем ресурсы и закрываем окно
cap.release()
cv2.destroyAllWindows()
