namespace Stats
{
    using System.Globalization;
    using ColossalFramework.UI;
    using ICities;
    using UnityEngine;

    public static class UIHelperExtensions
    {
        public static UISlider AddSliderWithLabel(
            this UIHelperBase uIHelper,
            string text,
            float min,
            float max,
            float step,
            float defaultValue,
            OnValueChanged onValueChangedCallback)
        {
            var spaceBetweenLabelAndSlider = 8f;
            var marginBottom = 16f;

            var sliderControl = uIHelper.AddSlider(text, min, max, step, defaultValue, _ => { }) as UISlider;
            var rootPanel = sliderControl!.parent as UIPanel;
            if (rootPanel is null)
                throw new IsNullException(nameof(rootPanel));

            rootPanel.autoLayout = false;

            var label = rootPanel.Find<UILabel>("Label");
            label.anchor = UIAnchorStyle.Left | UIAnchorStyle.Top;
            label.relativePosition = Vector3.zero;

            var slider = rootPanel.Find<UISlider>("Slider");
            slider.anchor = UIAnchorStyle.Left | UIAnchorStyle.Top;
            slider.relativePosition = PositionUnder(label);

            var valueLabel = rootPanel.AddUIComponent<UILabel>();
            valueLabel.name = "ValueLabel";
            valueLabel.text = defaultValue.ToString(CultureInfo.CurrentCulture);
            valueLabel.relativePosition = PositionRightOf(slider, spaceBetweenLabelAndSlider, 1f);

            rootPanel.height = label.height + spaceBetweenLabelAndSlider + slider.height + marginBottom;

            sliderControl.eventValueChanged += (_, value) =>
            {
                valueLabel.text = value.ToString(CultureInfo.CurrentCulture);
                onValueChangedCallback(value);
            };

            return sliderControl;
        }

        public static UIColorField AddColorFieldWithLabel(
            this UIHelperBase uIHelper,
            string text,
            Color32 color,
            PropertyChangedEventHandler<Color> selectedColorChanged)
        {
            var rootPanel = (uIHelper as UIHelper)?.self as UIPanel;
            if (rootPanel is null)
                throw new IsNullException(nameof(rootPanel));

            const float width = 25f;
            const float height = 25f;

            var colorPanel = rootPanel.AddUIComponent<UIPanel>();
            colorPanel.relativePosition = Vector3.zero;
            colorPanel.width = 500f;
            colorPanel.height = height;

            var colorLabel = colorPanel.AddUIComponent<UILabel>();
            colorLabel.text = text;
            colorLabel.relativePosition = new Vector3(width + 10f, 3f);
            colorLabel.textScale = 1.125f;

            var colorFieldTemplate = UITemplateManager
                .Get<UIPanel>("LineTemplate")
                .Find<UIColorField>("LineColor");
            if (colorFieldTemplate is null)
                throw new IsNullException(nameof(colorFieldTemplate));

            var colorField = UnityEngine.Object.Instantiate(colorFieldTemplate.gameObject)
                .GetComponent<UIColorField>();
            colorPanel.AttachUIComponent(colorField.gameObject);
            colorField.relativePosition = Vector3.zero;
            colorField.width = width;
            colorField.height = height;
            colorField.selectedColor = color;
            colorField.eventSelectedColorReleased += selectedColorChanged;

            var colorPicker = colorField.colorPicker;
            var colorPickerPanel = colorPicker.gameObject.GetComponent<UIPanel>();
            if (colorPickerPanel is null)
                throw new IsNullException(nameof(colorPickerPanel));

            var colorPickerHueUiSlider = colorPicker.m_HueSlider;
            if (colorPickerHueUiSlider is null)
                throw new IsNullException(nameof(colorPickerHueUiSlider));

            return colorField;
        }

        public static Vector3 PositionUnder(UIComponent uIComponent, float margin = 8f, float horizontalOffset = 0f)
        {
            return new Vector3(uIComponent.relativePosition.x + horizontalOffset, uIComponent.relativePosition.y + uIComponent.height + margin);
        }

        public static Vector3 PositionRightOf(UIComponent uIComponent, float margin = 8f, float verticalOffset = 0f)
        {
            return new Vector3(uIComponent.relativePosition.x + uIComponent.width + margin, uIComponent.relativePosition.y + verticalOffset);
        }
    }
}
