using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    public Text scoreText; // ссылка на текстовый объект для отображения счета
    public Text distanceText; // ссылка на текстовый объект для отображения расстояния
    private int score = 0; // счетчик уничтоженных объектов
    private Rigidbody rb; // ссылка на компонент Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // получаем компонент Rigidbody
        if (rb != null) // проверяем, что компонент существует
        {
            rb.freezeRotation = true; // замораживаем вращение
        }
    }

    void Update()
    {
        // Находим все объекты в радиусе 100 единиц вокруг капсулы
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 500);
        float minDistance = Mathf.Infinity; // начальное значение для минимального расстояния
        GameObject closestCube = null; // ссылка на ближайший объект с тегом "cube"

        // Перебираем все найденные объекты
        foreach (var hitCollider in hitColliders)
        {
            // Если объект имеет тег "cube"
            if (hitCollider.gameObject.tag == "cube")
            {
                // Вычисляем расстояние до объекта
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                // Если это расстояние меньше текущего минимального расстояния
                if (distance < minDistance)
                {
                    // Обновляем минимальное расстояние и ссылку на ближайший объект
                    minDistance = distance;
                    closestCube = hitCollider.gameObject;
                }
            }
        }

        // Если ближайший объект с тегом "cube" найден
        if (closestCube != null)
        {
            // Обновляем текстовый объект для отображения расстояния
            distanceText.text = "Расстояние: " + minDistance.ToString();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "cube")
        {
            Destroy(collision.gameObject);
            score++; // увеличиваем счетчик
            scoreText.text = "Уничтожено объектов: " + score.ToString(); // обновляем текст счетчика
        }
    }
}
