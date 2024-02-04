using UnityEngine;

public class SimpleScrollView : MonoBehaviour
{
    public RectTransform scrollArea; // Referência à área de rolagem
    public float scrollSpeed = 10f;
    public float deceleration = 8f; // Fator de desaceleração

    private Vector3 lastMousePosition;
    private bool isDragging = false;
    private float currentSpeed = 0f;

    void Update()
    {
        // Verifica se o botão do mouse foi pressionado e se o ponteiro está sobre a área de rolagem
        if (Input.GetMouseButtonDown(0) && IsPointerOverScrollArea())
        {
            isDragging = true;
        }

        if (isDragging)
        {
            // Calcula o deslocamento do mouse desde o último frame apenas no eixo Y
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollArea, Input.mousePosition, null, out Vector2 localPoint);
            float deltaY = deltaMousePosition.y * (-1) * scrollArea.rect.height / scrollArea.sizeDelta.y;

            // Move a área de rolagem apenas no eixo Y
            scrollArea.anchoredPosition -= new Vector2(0f, deltaY * scrollSpeed * Time.deltaTime);

            // Atualiza a velocidade atual para ser usada na desaceleração
            currentSpeed = deltaY / Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Aplica a desaceleração
        if (!isDragging)
        {
            scrollArea.anchoredPosition -= new Vector2(0f, currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Atualiza a posição do mouse para o próximo frame
        lastMousePosition = Input.mousePosition;
    }

    bool IsPointerOverScrollArea()
    {
        // Verifica se o ponteiro do mouse está sobre a área de rolagem
        RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollArea, Input.mousePosition, null, out Vector2 localPoint);
        return scrollArea.rect.Contains(localPoint);
    }
}
