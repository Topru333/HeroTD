
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;

[Serializable]
public class TowerGrid
{
    [SerializeField] private int _radius;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _scale;
    [SerializeField] private Color _lineColor;
    [SerializeField] private int _majorLines = 5;
    [field: SerializeField] public Color ColorAvailable { get; set; } = Color.green;
    [field: SerializeField] public Color ColorNotAvailable { get; set; } = Color.red;

    public int Radius
    {
        get
        {
            return _radius;
        }
    }

    public Vector3 Offset
    {
        get
        {
            return _offset;
        }
    }

    public float Scale
    {
        get
        {
            return _scale;
        }
    }

    public Color LineColor
    {
        get
        {
            return _lineColor;
        }
    }

    public TowerGrid(int radius, Vector3 offset, float scale, Color lineColor)
    {
        _radius = radius;
        _offset = offset;
        _scale = scale;
        _lineColor = lineColor;
    }

    public void DrawGizmos()
    {
        // set colours
        Color dimColor = new Color(LineColor.r, LineColor.g, LineColor.b, 0.25f * LineColor.a);
        Color brightColor = Color.Lerp(Color.white, LineColor, 0.75f);

        // draw the horizontal lines
        for (int number = Radius * -1; number < Radius + 1; number++)
        {
            // find major lines
            Gizmos.color = (number % _majorLines == 0 ? LineColor : dimColor);
            if (number == 0)
                Gizmos.color = brightColor;

            Vector3 pos1X = new Vector3(number, Radius * -1, 0) * Scale;
            Vector3 pos2X = new Vector3(number, Radius, 0) * Scale;

            Vector3 pos1Y = new Vector3(Radius * -1, number, 0) * Scale;
            Vector3 pos2Y = new Vector3(Radius, number, 0) * Scale;

            Gizmos.DrawLine((Offset + pos1X), (Offset + pos2X));
            Gizmos.DrawLine((Offset + pos1Y), (Offset + pos2Y));
        }
    }

    public Vector3 PointToTile(Vector3 point)
    {
        Vector3 result = Vector3.zero;
        float offset = Scale * .5f;
        result.x = (int)(point.x / Scale) * Scale + (point.x < 0 ? -offset : offset);
        result.y = (int)(point.y / Scale) * Scale + (point.y < 0 ? -offset : offset);
        return result;
    }

    private Vector3 GetWorldPosition(float x, float y, float z)
    {
        return new Vector3(x, y, z) * Scale;
    }

    private static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = TextAlignment.Center;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        return textMesh;
    }
}
