using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
<<<<<<< HEAD
=======
using WpfApp3.Container;
using lab_3; 
>>>>>>> de91eb4eebd989468b3587353b2e3eefcf00b76c

namespace WpfApp3.ViewModels
{
    public class MainViewModel : BaseViewModel
    public class MainViewModels : BaseViewModel
    {
        private readonly List<Container> _standardContainers;
        private Container? _selectedContainer;
        private bool _isCustomVolumeEnabled;
         private string _customVolume = string.Empty;
        private string _percentageContent = string.Empty;
        private string _numberOfContainers = string.Empty;
        private string _totalVolumeResult = string.Empty;
        private string _pureSubstanceVolumeResult = string.Empty;
        private string _errorMessage = string.Empty;

        public MainViewModel()
        public MainViewModels()
        {
            _standardContainers = new List<Models>
            {
                new Models("Wine Glass", 50),
                new Models("Cup", 250),
                new Models("Bottle", 500),
                new Models("Stack", 750),
                new Models("Laboratory Vessel", 1000),
                new Models("Custom", 0)
            };

            SelectedContainer = _standardContainers.First();
            CustomVolume = "";
            PercentageContent = "";
            NumberOfContainers = "";
            TotalVolumeResult = "";
            PureSubstanceVolumeResult = "";
            ErrorMessage = "";

            CalculateCommand = new Errors(Calculate, CanCalculate);
        }

        public List<Models> StandardContainers => _standardContainers;

        public Models? SelectedContainer
        {
            get => _selectedContainer;
            set
            {
                if (SetProperty(ref _selectedContainer, value))
                {
                    IsCustomVolumeEnabled = value?.Name == "Custom";
                    // Auto-fill volume field when selecting standard container
                    if (value != null && value.Name != "Custom" && value.VolumeMl > 0)
                    {
                        CustomVolume = value.VolumeMl.ToString();
                    }
                    CalculateCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsCustomVolumeEnabled
        {
            get => _isCustomVolumeEnabled;
            set => SetProperty(ref _isCustomVolumeEnabled, value);
        }

        public string CustomVolume
        {
            get => _customVolume;
            set
            {
                if (SetProperty(ref _customVolume, value))
                {
                    CalculateCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public string PercentageContent
        {
            get => _percentageContent;
            set
            {
                if (SetProperty(ref _percentageContent, value))
                {
                    CalculateCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public string NumberOfContainers
        {
            get => _numberOfContainers;
            set
            {
                if (SetProperty(ref _numberOfContainers, value))
                {
                    CalculateCommand?.RaiseCanExecuteChanged();
                }
            }
        }

        public string TotalVolumeResult
        {
            get => _totalVolumeResult;
            set => SetProperty(ref _totalVolumeResult, value);
        }

        public string PureSubstanceVolumeResult
        {
            get => _pureSubstanceVolumeResult;
            set => SetProperty(ref _pureSubstanceVolumeResult, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public Errors CalculateCommand { get; }

        private bool CanCalculate()
        {
            ClearErrors();

            if (SelectedContainer == null)
            {
                return false;
            }

            if (SelectedContainer.Name == "Custom" && (string.IsNullOrWhiteSpace(CustomVolume) || !double.TryParse(CustomVolume, out var customVol) || customVol <= 0))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(PercentageContent) || !double.TryParse(PercentageContent, out var percentage))
            {
                return false;
            }

            if (percentage < 0 || percentage > 100)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(NumberOfContainers) || !int.TryParse(NumberOfContainers, out var count) || count <= 0)
            {
                return false;
            }

            return true;
        }

        private void Calculate()
        {
            ClearErrors();

            if (!CanCalculate())
            {
                ErrorMessage = "Please fill in all required fields with valid values.";
                return;
            }

            try
            {
                double containerCapacity;

                if (SelectedContainer?.Name == "Custom")
                {
                    if (!double.TryParse(CustomVolume, out containerCapacity) || containerCapacity <= 0)
                    {
                        ErrorMessage = "Custom volume must be a positive number.";
                        return;
                    }
                }
                else
                {
                    containerCapacity = SelectedContainer?.VolumeMl ?? 0;
                }

                if (!double.TryParse(PercentageContent, out var percentage))
                {
                    ErrorMessage = "Percentage content must be a valid number.";
                    return;
                }

                if (percentage < 0 || percentage > 100)
                {
                    ErrorMessage = "Percentage content must be between 0 and 100.";
                    return;
                }

                if (!int.TryParse(NumberOfContainers, out var numberOfContainers) || numberOfContainers <= 0)
                {
                    ErrorMessage = "Number of containers must be a positive integer.";
                    return;
                }

                var totalVolume = containerCapacity * numberOfContainers;
                var pureSubstanceVolume = totalVolume * (percentage / 100.0);

                TotalVolumeResult = $"Total fluid volume: {totalVolume:F2} ml";
                PureSubstanceVolumeResult = $"Pure substance volume: {pureSubstanceVolume:F2} ml";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred during calculation: {ex.Message}";
            }
        }

        private void ClearErrors()
        {
            ErrorMessage = "";
        }
    }
}
