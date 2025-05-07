namespace CSVConverter.BusinessLogic
{
    public class TwoStrings
    {
        public string recordOne { get; set; }
        public string recordTwo { get; set; }

        public TwoStrings()
        {
            recordOne = "";
            recordTwo = "";
        }

        public TwoStrings(string _recordOne, string _recordTwo)
        {
            this.recordOne = _recordOne;
            this.recordTwo = _recordTwo;
        }
    }
}
