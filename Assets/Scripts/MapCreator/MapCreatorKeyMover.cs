using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapCreatorKeyMover : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _inputMousePosition;

    [SerializeField] private MapCreatorDataSaver dataSaver;
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    private Transform _selectedKey;
    private Vector2 _offset;

    private Coroutine _moveKeyCoroutine;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Actions.Click.started += _ => StartMoving();
        _inputMaster.Actions.Click.canceled += _ => CancelMoving();

        _inputMousePosition = _inputMaster.Actions.MousePosition;
    }

    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = _inputMousePosition.ReadValue<Vector2>();
        return mousePosition;
    }

    public void StartMoving(Transform selectedKey = null)
    {
        if (selectedKey == null)
        {
            List<RaycastResult> results = RaycastUtilities.GetRaycastResults(GetMousePosition(), graphicRaycaster);

            if (!results.Exists(key => key.gameObject.TryGetComponent(out MapCreatorKey _))) return;

            _selectedKey = results.First(key => key.gameObject.TryGetComponent(out MapCreatorKey _)).gameObject.transform;
        }
        else _selectedKey = selectedKey;

        Vector3 mousePosition = GetMousePosition();
        _offset = _selectedKey.position - mousePosition;

        _moveKeyCoroutine = StartCoroutine(MoveKeyCoroutine());
    }
    
    private void CancelMoving()
    {
        if (_selectedKey == null) return;

        List<RaycastResult> results = RaycastUtilities.GetRaycastResults(GetMousePosition(), graphicRaycaster);

        if (results.Exists(key => key.gameObject.TryGetComponent(out MapCreatorDeleteKeyZone _)))
        {
            dataSaver.RemoveKeyData(_selectedKey.gameObject.GetComponent<MapCreatorKey>().KeyData.Id);
            Destroy(_selectedKey.gameObject);
        }
        else
        {
            _selectedKey.gameObject.GetComponent<MapCreatorKey>().UpdatePosition();
        }

        _selectedKey = null;
        _offset = Vector2.zero;

        StopCoroutine(_moveKeyCoroutine);
        _moveKeyCoroutine = null;
    }

    private IEnumerator MoveKeyCoroutine()
    {
        while (true)
        {
            Vector2 mousePosition = GetMousePosition();
            _selectedKey.position = mousePosition + _offset;

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Disable();

        _inputMaster.Actions.Click.started -= _ => StartMoving();
        _inputMaster.Actions.Click.canceled -= _ => CancelMoving();
    }
}
