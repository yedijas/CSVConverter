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
            newString = new TwoStrings();
            TransformString();
        }

        /// <summary>
        /// Transforms the old string to new values.
        /// </summary>
        /// <returns>Updated values</returns>
        public void TransformString ()
        {
            TransformString(oldString);
        }

        /// <summary>
        /// Transforms the old string to new values.
        /// </summary>
        /// <returns>Updated values</returns>
        public void TransformString(TwoStrings _oldString)
        {
            // null check first
            if (_oldString != null)
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
                            newString.recordOne = "1";
                            newString.recordTwo = "1";
                            break;
                        case "N":
                            newString.recordOne = "1";
                            newString.recordTwo = "2";
                            break;
                        case "R":
                            newString.recordOne = "2";
                            newString.recordTwo = "1";
                            break;
                        case "H":
                            newString.recordOne = "2";
                            newString.recordTwo = "2";
                            break;
                        case "3":
                            newString.recordOne = "3";
                            newString.recordTwo = "1";
                            break;
                        default:
                            // default it to individual residence
                            newString.recordOne = _oldString.recordOne;
                            if (!Regex.IsMatch(_oldString.recordOne, @"^[1-3]"))
                            {
                                newString.recordOne = "1";

                            }
                            newString.recordTwo = "1";
                            break;
                    }
                }
                else
                {
                    if (_oldString.recordOne.Equals("4") || _oldString.recordOne.Equals("99"))
                    {
                        // default it to individual residence
                        newString.recordOne = "1";
                        newString.recordTwo = "1";
                    }
                }
            }
        }
    }
}
