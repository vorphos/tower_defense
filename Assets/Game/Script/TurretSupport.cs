using UnityEngine;

public class TurretSupport : MonoBehaviour
{
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color occupiedColor = Color.red;

    private GameObject _turret;
    private Renderer _rend;
    private Color baseColor;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        baseColor = _rend.material.color;
    }

    public bool HasTurret()
    {
        return _turret != null;
    }

    public void Hover()
    {
        if (_turret == null)
        {
            _rend.material.color = hoverColor;
        }
        else
        {
            _rend.material.color = baseColor;
        }
    }

    public void StopHover()
    {
        _rend.material.color = baseColor;
    }
    
    public void BuildTurret(GameObject turretPrefab)
    {
        Vector3 position = transform.position + Vector3.up * (transform.localScale.y / 2f);
        _turret = Instantiate(turretPrefab, position, Quaternion.identity);
    }
}
