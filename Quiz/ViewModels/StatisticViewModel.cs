using DAL.Entityes;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using Quiz.Models;

namespace Quiz.ViewModels
{
    class StatisticViewModel : BaseViewModel
    {
        public StatisticViewModel()
        {
            _statisticsRepository = App.Services.GetRequiredService<StatisticsRepository>();
            GetStatistics();
            CustomiseChartModel();
        }

        /// <summary>Репозиторий ститистики</summary>
        private readonly StatisticsRepository _statisticsRepository;

        /// <summary>Набор данных ститистики для отображения</summary>
        private List<StatisticItem> _statisticsList;

        /// <summary>Набор данных ститистики для отображения</summary>
        public List<StatisticItem> StatisticsList
        {
            get { return _statisticsList; }
            set
            {
                Set(ref _statisticsList, value);
                if (!(_statisticsList == null) && _statisticsList.Count > 0)
                    TestsCount = _statisticsList[0].RigthAnswers + _statisticsList[0].WrongAnswers;
            }
        }

        /// <summary>Количество проведенных тестов</summary>
        private int _testsCount = 0;

        /// <summary>Количество проведенных тестов</summary>
        public int TestsCount
        {
            get { return _testsCount; }
            set { Set(ref _testsCount, value); }
        }

        /// <summary>Модель для отображения графика</summary>
        public PlotModel ChartModel { get; private set; }


        /// <summary>Настройка отображения графика</summary>
        private void CustomiseChartModel()
        {
            var model = new PlotModel
            {
                Title = "Статистика ответов",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightMiddle,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };

            var s1 = new ColumnSeries { Title = "Правильно", FillColor = OxyColors.Blue, IsStacked = true, StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            var s2 = new ColumnSeries { Title = "Не правильно", FillColor = OxyColors.Red, IsStacked = true, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

            foreach (var item in StatisticsList)
            {
                s1.Items.Add(new ColumnItem { Value = item.RigthAnswers });
                s2.Items.Add(new ColumnItem { Value = item.WrongAnswers });
            }

            var xAxis = new CategoryAxis { Position = AxisPosition.Bottom };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            ChartModel = model;
        }

        /// <summary>Сбор данных статистики</summary>
        private void GetStatistics()
        {
            // Данные по правильным ответам
            var rightAnswers = _statisticsRepository.Stats.Where(p => p.Answer.IsRight == true)
                .OrderBy(p => p.Answer.Question.Id).ToList()
                .GroupBy(p => p.Answer.Question)
                .Select(g => new
                {
                    Key = g.Key,
                    Count = g.Count()
                }).ToDictionary(k => k.Key, k => k.Count);

            // Данные по не правильным ответам
            var wrongAnswers
                = _statisticsRepository.Stats.Where(p => p.Answer.IsRight == false)
                .OrderBy(p => p.Answer.Question.Id).ToList()
                .GroupBy(p => p.Answer.Question)
                .Select(g => new
                {
                    Key = g.Key,
                    Count = g.Count()
                }).ToDictionary(k => k.Key, k => k.Count);

            // Объединение ключей
            List<Question> keys = rightAnswers.Keys.Union(wrongAnswers.Keys)
                .OrderBy(o => o.Id)
                .ToList();

            // Объединение данных
            List<StatisticItem> items = new List<StatisticItem>();
            foreach (var key in keys)
            {
                int value;
                StatisticItem newItem = new StatisticItem
                {
                    Question = key.Text,
                    RigthAnswers = rightAnswers.TryGetValue(key, out value) ? value : 0,
                    WrongAnswers = wrongAnswers.TryGetValue(key, out value) ? value : 0
                };
                items.Add(newItem);
            }

            StatisticsList = items;
        }
    }
}
