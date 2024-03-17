namespace IntelmetTestTask
{
    public class MeasureHelper
    {
        private const double DefaultRequiredSnr1 = 0.0;
        private const double DefaultSpeedThreshold = 0.0;

        public double RequiredSnr1 { get; set; } = DefaultRequiredSnr1;
        public double SpeedThreshold { get; set; } = DefaultSpeedThreshold;
        
        public List<Range> GetRangesWithoutNoise(Measure measure)   // Returns the list of ranges at which the average SNR1 for each time is greater or equal than RequiredSnr1
        {
            if (measure is null)
            {
                throw new ArgumentNullException(nameof(measure));
            }

            var entryGroups = measure.Distances.GroupBy(entry => entry.Time).ToList();  // group entries by time
            entryGroups.Sort((g1, g2) => DateTime.Compare(g1.Key, g2.Key));   // sort entry groups by time
            var ranges = new List<Range>();
            Range? currentRange = null;

            for (int i = 0; i < entryGroups.Count; i++)
            {
                if (EntryGroupIsValid(entryGroups[i]))
                {
                    currentRange = new Range(entryGroups[i].Key, entryGroups[i].Key);

                    for (i = i + 1; i < entryGroups.Count; i++)
                    {
                        if (!EntryGroupIsValid(entryGroups[i]))
                        {
                            break;
                        }
                    }

                    currentRange.End = entryGroups[i - 1].Key;
                    ranges.Add(currentRange);
                }
            }

            ranges.RemoveAll(r => r.Start == r.End);    // remove ranges that contain only one moment of time
            return ranges;
        }

        private bool EntryGroupIsValid(IGrouping<DateTime, DistanceEntry> entryGroup)
        {
            double averageSpeed = entryGroup.Average(entry => entry.Speed);
            double averageSnr1 = entryGroup.Average(entry => entry.Snr1);
            return averageSpeed >= SpeedThreshold && averageSnr1 >= RequiredSnr1;
        }
    }
}
