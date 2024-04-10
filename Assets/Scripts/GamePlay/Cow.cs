using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Cow : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private GameObject _deadCowProfab;
    [SerializeField] private float _minJumpTime = 1f;
    [SerializeField] private float _maxJumpTime = 2f;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _jumpTimer = 1f;
    private bool _isCatched = false;

    public void Catched()
    {
        _isCatched = true;
        _animator.SetBool("Fly", true);
        _rigidbody.isKinematic = true;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Update()
    {
        if (!_isCatched)
            TryJump();
    }

    private void TryJump()
    {
        if (_jumpTimer > 0)
        {
            _jumpTimer -= Time.deltaTime;

            if (_jumpTimer <= 0)
            {
                Jump();
                _jumpTimer = Random.Range(_minJumpTime, _maxJumpTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var attachedRigidbody = collision.collider.attachedRigidbody;

        if (_isCatched)
            return;

        if(attachedRigidbody == null)
            return;

        if (attachedRigidbody.GetComponent<Player>() != null)
        {
            Instantiate(_deadCowProfab, _transform.position,_transform.rotation);
            Destroy(gameObject);
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody.velocity = (Vector3.up + _transform.forward) * _jumpPower;
    }
}
