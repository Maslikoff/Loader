using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera; // ������ ������
    [SerializeField] private Transform _holdPosition; // ���������, ��� ����� ������������ �������
    [SerializeField] private float _rayDistance = 2.0f; // ��������� ��� ����

    private GameObject _heldItem = null; // ������� ������������ �������

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

            // ���������� ��� �� ������ ������
            if (Physics.Raycast(ray, out hit, _rayDistance))
            {
                if (hit.collider.CompareTag("Item")) // ���������, ��� ��������������� � ���������
                {
                    _heldItem = hit.collider.gameObject;
                    _heldItem.transform.SetParent(_holdPosition); // ������ ������� �������� �������� ����� �������
                    _heldItem.transform.localPosition = Vector3.zero; // ���������� ������� ������������ �����
                    _heldItem.GetComponent<Rigidbody>().isKinematic = true; // ��������� ������ ��� ��������
                }
            }
        }
    }

    public void TryPlaceItem()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;

        // ���������� ��� �� ������ ������
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider.CompareTag("CarTrunk")) // ���������, ��� ��������������� � ���������� ������
            {
                TrunkManager trunk = hit.collider.GetComponent<TrunkManager>();
                if (trunk != null && trunk.TryAddItem(_heldItem)) // �������� �������� ������� � ��������
                {
                    _heldItem.GetComponent<Rigidbody>().isKinematic = false; // �������� ������ ��� ��������
                    _heldItem.transform.SetParent(null); // ������� ��������
                    _heldItem = null; // ������� ������� �������
                }
            }
        }
    }
}
