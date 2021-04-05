using System;
using System.Collections.Generic;

namespace Quiz.ViewModels
{
    class ResultViewModel : BaseViewModel
    {
        public ResultViewModel() { }

        public ResultViewModel(Dictionary<String, bool> result)
        {
            Result = result;
        }

        /// <summary>Результат тестирования</summary>
        public Dictionary<String, bool> Result { get; set; }
    }
}
