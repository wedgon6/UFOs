using UnityEngine;

public class CowCather : MonoBehaviour
{
    [SerializeField] private float _cathDistance;
    [SerializeField] private float _cathRadius;
    [SerializeField] private GameObject _effect;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _catchTime;

    private bool _isCatchActionActive = false;
    private Transform _trasform;
    private float _catchTimer = -1f;
    private Transform _cathedCow;
    private Vector3 _startCowPosition;
    private Vector3 _startCowScale;

    private void Awake()
    {
        _trasform = transform;
    }

    public void SetInpyt(PlayerInpyt inpyt)
    {
        inpyt.CatchPressed += OnCatchPressed;
        inpyt.CatchReleased += OnCatchRelesed;
    }

    private void OnCatchRelesed()
    {
        if (_cathedCow != null)
            return;

        SetCatch(false);
    }

    private void OnCatchPressed()
    {
        SetCatch(true);
    }

    private void SetCatch(bool isActive)
    {
        _effect.SetActive(isActive);
        _isCatchActionActive = isActive;
    }


    private void FixedUpdate()
    {
        if (!_isCatchActionActive)
            return;

        if (_cathedCow != null)
            return;

        var colliders = Physics.OverlapSphere(_trasform.position + _trasform.forward * _cathDistance, _cathRadius, _layerMask,QueryTriggerInteraction.Ignore);

        foreach (var collider in colliders)
        {
            var cow = collider.GetComponentInParent<Cow>();

            if (cow != null)
            {
                cow.Catched();
                _cathedCow = cow.transform;
                _cathedCow.SetParent(_trasform);
                _startCowPosition = _cathedCow.localPosition;
                _startCowScale = _cathedCow.localScale;

                _catchTimer = 1f;
                break;
            }
        }
    }

    private void Update()
    {
        if(_catchTimer > 0)
        {
            _catchTimer -= Time.deltaTime / _catchTime;
            
            if(_catchTimer <= 0)
            {
                if(_cathedCow != null)
                {
                    Destroy(_cathedCow.gameObject);
                    _cathedCow = null;
                    OnCatchRelesed();
                }
            }
        }

        if(_cathedCow != null)
        {
            UpdateCowTransform();
        }
    }

    private void UpdateCowTransform()
    {
        float time = Mathf.SmoothStep(0,1,_catchTimer);

        _cathedCow.transform.localPosition = Vector3.Lerp(Vector3.zero,_startCowPosition, time);
        _cathedCow.transform.localScale = Vector3.Lerp(Vector3.zero, _startCowScale, time);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward *_cathDistance, _cathRadius);
    }
}
