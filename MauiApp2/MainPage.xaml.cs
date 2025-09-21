namespace MauiApp2;

public partial class MainPage
{
    private readonly BenchmarkService _benchmarkService;

    public MainPage(BenchmarkService benchmarkService)
    {
        InitializeComponent();
        _benchmarkService = benchmarkService;
    }

    private async void OnFibonacciClicked(object sender, EventArgs e)
    {
        SetUiState(true, "Executando benchmark Fibonacci");

        var result = await _benchmarkService.RunFibonacciBenchmarkAsync(40);
        ResultsEditor.Text = result;

        SetUiState(false);
    }

    private async void OnApiBenchClicked(object sender, EventArgs e)
    {
        SetUiState(true, "Executando benchmark API");

        var result = await _benchmarkService.RunApiBenchmarkAsync();
        ResultsEditor.Text = result;

        SetUiState(false);
    }

    private void SetUiState(bool isLoading, string initialText = "")
    {
        LoadingIndicator.IsRunning = isLoading;
        FibButton.IsEnabled = !isLoading;
        ApiButton.IsEnabled = !isLoading;

        if (isLoading)
        {
            ResultsEditor.Text = initialText;
        }
    }
}