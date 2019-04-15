using UnityEngine;
using InkPainterExtension;

namespace InkPainterSharingSample
{
    public class MouseInputHandler : MonoBehaviour
    {
        InkPainterExtension.InkPainter painter;
        ColorPickerTriangle colorPicker;

        void Start()
        {
            painter = transform.GetComponent<InkPainterExtension.InkPainter>();
            colorPicker = Camera.main.GetComponentInChildren<ColorPickerTriangle>();
        }

        void Update()
        {
            if(Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                painter.ray.origin = ray.origin;
                painter.ray.direction = ray.direction;
                painter.paintEnabled = true;
                painter.brushColor = colorPicker.TheColor;
            }
            else
            {
                painter.paintEnabled = false;
            }
        }
    }
}