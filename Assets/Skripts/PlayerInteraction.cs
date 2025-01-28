using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera; // Камера игрока
    [SerializeField] private Transform _holdPosition; // Положение, где будет удерживаться предмет
    [SerializeField] private float _rayDistance = 2.0f; // Дистанция для луча

    private GameObject _heldItem = null; // Текущий удерживаемый предмет

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_heldItem == null)
            {
                TryPickupItem();
            }
            else
            {
                TryPlaceItem();
            }
        }
    }

    public void TryPickupItem()
    {
        if (_heldItem == null)
        {
            Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
            RaycastHit hit;

            // Отправляем луч из центра камеры
            if (Physics.Raycast(ray, out hit, _rayDistance))
            {
                if (hit.collider.CompareTag("Item")) // Проверяем, что взаимодействуем с предметом
                {
                    _heldItem = hit.collider.gameObject;
                    _heldItem.transform.SetParent(_holdPosition); // Делаем предмет дочерним объектом точки захвата
                    _heldItem.transform.localPosition = Vector3.zero; // Сбрасываем позицию относительно точки
                    _heldItem.GetComponent<Rigidbody>().isKinematic = true; // Отключаем физику для предмета
                }
            }
        }
    }

    public void TryPlaceItem()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;

        // Отправляем луч из центра камеры
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider.CompareTag("CarTrunk")) // Проверяем, что взаимодействуем с багажником машины
            {
                TrunkManager trunk = hit.collider.GetComponent<TrunkManager>();
                if (trunk != null && trunk.TryAddItem(_heldItem)) // Пытаемся положить предмет в багажник
                {
                    _heldItem.GetComponent<Rigidbody>().isKinematic = false; // Включаем физику для предмета
                    _heldItem.transform.SetParent(null); // Убираем родителя
                    _heldItem = null; // Очищаем текущий предмет
                }
            }
        }
    }
}
