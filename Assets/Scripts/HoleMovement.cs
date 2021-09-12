using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [Header("Hole mesh")]
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshCollider _meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField] Vector2 _moveLimits;
    [SerializeField] private float _radius;
    [SerializeField] private Transform _holeCenter;

    [Space]
    [SerializeField] private float _moveSpeed;

    private Mesh _mesh;
    private List<int> _holeVertices;
    private List<Vector3> _offSets;
    private int _holeVerticesCount;

    private float _x, _y;
    private Vector3 _touch, _targetPosition;

    private void Start()
    {
        Game.isMoving = false;
        Game.isGameOver = false;

        _holeVertices = new List<int>();
        _offSets = new List<Vector3>();

        _mesh = _meshFilter.mesh;

        FindHoleVectices();


    }
    private void Update()
    {
        Game.isMoving = Input.GetMouseButton(0);

        if(!Game.isGameOver && Game.isMoving)
        {
            MoveHole();

            UpdateVerticesHolePostion();
        }
    }
    private void MoveHole()
    {
        _x = Input.GetAxis("Mouse X");
        _y = Input.GetAxis("Mouse Y");

        _touch = Vector3.Lerp(_holeCenter.position, _holeCenter.position + new Vector3(_x, 0f, _y), _moveSpeed * Time.deltaTime);

        _targetPosition = new Vector3(
            Mathf.Clamp(_touch.x, -_moveLimits.x, _moveLimits.x), _touch.y,
            Mathf.Clamp(_touch.z, -_moveLimits.y, _moveLimits.y)
        );
        _holeCenter.position = _targetPosition;
    }

    private void UpdateVerticesHolePostion()
    {
        Vector3[] vertices = _mesh.vertices;
        for (int i = 0; i < _holeVerticesCount; i++)
        {
            vertices [_holeVertices [i]] = _holeCenter.position + _offSets[i];
        }

        _mesh.vertices = vertices;
        _meshFilter.mesh = _mesh;
        _meshCollider.sharedMesh = _mesh;
    }


    private void FindHoleVectices()
    {
        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            float distace = Vector3.Distance(_holeCenter.position, _mesh.vertices[i]);

            if(distace < _radius)
            {
                _holeVertices.Add(i);
                _offSets.Add(_mesh.vertices[i] - _holeCenter.position);
            }
        }

        _holeVerticesCount = _holeVertices.Count;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_holeCenter.position, _radius);
    }
}
