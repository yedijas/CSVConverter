using System;
using System.Text.RegularExpressions;

namespace CSVConverter.BusinessLogic
{
    public class MainConverter
    {
        public TwoStrings oldString;
        public TwoStrings newString;

        public MainConverter()
        {
            oldString = new TwoStrings();
            newString = new TwoStrings();
        }

        public MainConverter(string recordOne, string recordTwo)
        {
            oldString = new TwoStrings(recordOne, recordTwo);
            newString = TransformString();
        }

        /// <summary>
        /// Transforms the old string to new values.
        /// </summary>
        /// <returns>Updated values</returns>
        public TwoStrings TransformString ()
        {
            return TransformString(oldString);
        }

        /// <summary>
        /// Transforms the old string to new values.
        /// </summary>
        /// <returns>Updated values</returns>
        public TwoStrings TransformString(TwoStrings _oldString)
        {
            TwoStrings _newString = new TwoStrings();

            // null check first
            if (oldString != null)
            {
                // values of the params to replace
                if (Regex.IsMatch(_oldString.recordTwo, @"^[a-zA-Z]+$")
                    || String.IsNullOrEmpty(_oldString.recordTwo)
                    || Regex.IsMatch(_oldString.recordOne, @"^[ynrhYNRH3]"))
                {
                    // here means that record two is not number
                    // or if it is a number, record one still match the new logic
                    // then translate value from record one to match the new logic
                    switch (_oldString.recordOne.ToUpperInvariant())
                    {
                        case "Y":
                            _newString.recordOne = "1";
                            _newString.recordTwo = "1";
                            break;
                        case "N":
                            _newString.recordOne = "1";
                            _newString.recordTwo = "2";
                            break;
                        case "R":
                            _newString.recordOne = "2";
                            _newString.recordTwo = "1";
                            break;
                        case "H":
                            _newString.recordOne = "2";
                            _newString.recordTwo = "2";
                            break;
                        case "3":
                            _newString.recordOne = "3";
                            _newString.recordTwo = "1";
                            break;
                        default:
                            // default it to individual residence
                            _newString.recordOne = _oldString.recordOne;
                            if (!Regex.IsMatch(_oldString.recordOne, @"^[1-3]"))
                            {
                                _newString.recordOne = "1";

                            }
                            _newString.recordTwo = "1";
                            break;
                    }
                }
                else
                {
                    if (_oldString.recordOne.Equals("4") || _oldString.recordOne.Equals("99"))
                    {
                        // default it to individual residence
                        _newString.recordOne = "1";
                        _newString.recordTwo = "1";
                    }
                }
            }

            return _newString;
        }
    }
}
