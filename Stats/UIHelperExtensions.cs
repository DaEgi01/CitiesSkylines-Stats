using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace Stats
{
    public static class UIHelperExtensions
    {
        public static UISlider AddSliderWithLabel(this UIHelperBase uIHelper, string text, float min, float max, float step, float defaultValue, OnValueChanged eventCallback)
        {
            float spaceBetweenLabelAndSlider = 8f;
            float marginBottom = 16f;

            var sliderControl = uIHelper.AddSlider(text, min, max, step, defaultValue, value => { }) as UISlider;
            var rootPanel = sliderControl.parent as UIPanel;
            rootPanel.autoLayout = false;

            var label = rootPanel.Find<UILabel>("Label");
            label.anchor = UIAnchorStyle.Left | UIAnchorStyle.Top;
            label.relativePosition = Vector3.zero;

            var slider = rootPanel.Find<UISlider>("Slider");
            slider.anchor = UIAnchorStyle.Left | UIAnchorStyle.Top;
            slider.relativePosition = PositionUnder(label);

            var valueLabel = rootPanel.AddUIComponent<UILabel>();
            valueLabel.name = "ValueLabel";
            valueLabel.text = defaultValue.ToString();
            valueLabel.relativePosition = PositionRightOf(slider, spaceBetweenLabelAndSlider, 1f);

            rootPanel.height = label.height + spaceBetweenLabelAndSlider + slider.height + marginBottom;

            sliderControl.eventValueChanged += (comp, value) => {
                valueLabel.text = value.ToString();
                eventCallback(value);
            };

            return sliderControl;

            Vector3 PositionUnder(UIComponent uIComponent, float margin = 8f, float horizontalOffset = 0f)
            {
                return new Vector3(uIComponent.relativePosition.x + horizontalOffset, uIComponent.relativePosition.y + uIComponent.height + margin);
            }

            Vector3 PositionRightOf(UIComponent uIComponent, float margin = 8f, float verticalOffset = 0f)
            {
                return new Vector3(uIComponent.relativePosition.x + uIComponent.width + margin, uIComponent.relativePosition.y + verticalOffset);
            }
        }
    }
}
