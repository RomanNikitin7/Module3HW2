using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3HW2
{
    public class PhoneBook : IEnumerable
    {
        public const string EnglishCultureName = "en-GB";
        public const string UkrainianCultureName = "uk";
        private const string NotSupportedLetterChapter = "#";
        private const string NumbersChapter = "0-9";
        private const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string UkrainianAlphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
        private Dictionary<string, List<IContact>> _english;
        private Dictionary<string, List<IContact>> _ukrainian;

        public PhoneBook()
        {
            _english = new Dictionary<string, List<IContact>>();
            _ukrainian = new Dictionary<string, List<IContact>>();
        }

        public void Add(IContact item, CultureInfo info)
        {
            if (info == null)
            {
                info = new CultureInfo(EnglishCultureName);
            }
            if (info.Name.Equals(EnglishCultureName, StringComparison.InvariantCultureIgnoreCase))
            {
                AddEnglishContact(item, info);
            } else if (info.Name.Equals(UkrainianCultureName, StringComparison.InvariantCultureIgnoreCase))
            {
                AddUkrainianContact(item, info);
            }
        }
        
        private void AddEnglishContact(IContact item, CultureInfo info)
        {
            if (IsDigit(item))
            {
                if (!_english.ContainsKey(NumbersChapter))
                {
                    _english.Add(NumbersChapter, new List<IContact>());
                }
                _english[NumbersChapter].Add(item);
                _english[NumbersChapter].Sort();
                return;
            }
            if (IsNotSupportedLetter(item, info))
            {
                if (!_english.ContainsKey(NotSupportedLetterChapter))
                {
                    _english.Add(NotSupportedLetterChapter, new List<IContact>());
                }
                _english[NotSupportedLetterChapter].Add(item);
                _english[NotSupportedLetterChapter].Sort();
                return;
            }
            if (!_english.ContainsKey(GetChapterLetter(item)))
            {
                _english.Add(GetChapterLetter(item), new List<IContact>());
            }
            _english[GetChapterLetter(item)].Add(item);
            _english[GetChapterLetter(item)].Sort();
        }
        private void AddUkrainianContact(IContact item, CultureInfo info)
        {
            if (IsDigit(item))
            {
                if (!_ukrainian.ContainsKey(NumbersChapter))
                {
                    _ukrainian.Add(NumbersChapter, new List<IContact>());
                }
                _ukrainian[NumbersChapter].Add(item);
                _ukrainian[NumbersChapter].Sort();
                return;
            }
            if (IsNotSupportedLetter(item, info))
            {
                if (!_ukrainian.ContainsKey(NotSupportedLetterChapter))
                {
                    _ukrainian.Add(NotSupportedLetterChapter, new List<IContact>());
                }
                _ukrainian[NotSupportedLetterChapter].Add(item);
                _ukrainian[NotSupportedLetterChapter].Sort();
                return;
            }
            if (!_ukrainian.ContainsKey(GetChapterLetter(item)))
            {
                _ukrainian.Add(GetChapterLetter(item), new List<IContact>());
            }
            _ukrainian[GetChapterLetter(item)].Add(item);
            _ukrainian[GetChapterLetter(item)].Sort();
        }
        private bool IsDigit(IContact item)
        {
            if(item.GetKey().Length > 1)
                return char.IsDigit(item.GetKey()[0]);
            return false;
        }

        private bool IsNotSupportedLetter(IContact item, CultureInfo info)
        {
            if (item.GetKey().Length < 1)
            {
                return false;
            }
            if (info.Name.Equals(EnglishCultureName, StringComparison.InvariantCultureIgnoreCase))
            {
                return !EnglishAlphabet.Contains(item.GetKey()[0].ToString().ToUpper());
            }

            if (info.Name.Equals(UkrainianCultureName, StringComparison.InvariantCultureIgnoreCase))
            {
                return !UkrainianAlphabet.Contains(item.GetKey()[0].ToString().ToUpper());
            }

            return false;
        }
        
        private string GetChapterLetter(IContact item)
        {
            if (item.GetKey().Length < 1)
            {
                return string.Empty;
            }
            return item.GetKey()[0].ToString().ToUpper();
        }

        public IEnumerator GetEnumerator()
        {
            return new PhoneBookEnumerator(_english, _ukrainian);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private class PhoneBookEnumerator : IEnumerator
        {
            private int _countEn = -1;
            private int _countUk = -1;
            private Dictionary<string, List<IContact>> _dataEn;
            private Dictionary<string, List<IContact>> _dataUk;

            public PhoneBookEnumerator(Dictionary<string, List<IContact>> en, Dictionary<string, List<IContact>> uk)
            {
                _dataEn = en;
                _dataUk = uk;
            }
            public void Reset()
            {
                _countEn = -1;
                _countUk = -1;
            }
            public object Current
            {
                get
                {
                    int count = 0;
                    if (_countEn <= _dataEn.Count - 1 && _countUk == -1)
                    {
                        foreach(var item in _dataEn)
                        {
                            if (count == _countEn)
                            {
                                return item;
                            }
                            count++;
                        }
                    }

                    foreach (var item in _dataUk)
                    {
                        if (count == _countUk)
                        {
                            return item;
                        }
                        count++;
                    }

                    return null;
                }
            }

            public bool MoveNext()
            {
                if (_countEn < _dataEn.Count - 1)
                {
                    _countEn++;
                    return true;
                }
                if (_countUk < _dataUk.Count - 1)
                {
                    _countUk++;
                    return true;
                }

                return false;
            }
        }
    }
}
