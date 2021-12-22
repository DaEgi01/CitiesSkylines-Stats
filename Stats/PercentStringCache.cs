using System;

namespace Stats
{
    public class PercentStringCache
    {
        private readonly int _minValue;
        private readonly int _maxValue;

        private readonly string _nullString = "-%";
        private readonly string _negativeOutOfRangeString;
        private readonly string _positiveOutOfRangeString;
        private readonly string[] _inRangeStringCache;

        public PercentStringCache(int minValue, int maxValue)
        {
            if (minValue >= 0)
                throw new ArgumentOutOfRangeException(nameof(minValue), "Must be smaller than 0.");

            if (maxValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(minValue), "Must be larger than 0.");

            _minValue = minValue;
            _maxValue = maxValue;

            _negativeOutOfRangeString = $"<-{minValue.ToString()}%";
            _positiveOutOfRangeString = $">{maxValue.ToString()}%";
            _inRangeStringCache = new string[-minValue + maxValue + 1];
            FillInRangeStringCache(_inRangeStringCache, _minValue, _maxValue);
        }

        private void FillInRangeStringCache(string[] inRangeStringCache, int minValue, int maxValue)
        {
            for (int i = minValue; i < maxValue + 1; i++)
            {
                inRangeStringCache[GetIndex(i)] = i.ToString() + "%";
            }
        }

        public string PositiveOutOfRangeString => _positiveOutOfRangeString;

        public string NegativeOutOfRangeString => _negativeOutOfRangeString;

        public string GetPercentString(int? value)
        {
            if (!value.HasValue)
                return _nullString;

            if (value > _maxValue)
                return _positiveOutOfRangeString;

            if (value < _minValue)
                return _negativeOutOfRangeString;

            return _inRangeStringCache[GetIndex(value.Value)];
        }

        private int GetIndex(int value)
        {
            if (value >= 0)
                return value;

            return value + _maxValue;
        }
    }
}
