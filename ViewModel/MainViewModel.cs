using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace ToDoList.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity) 
        {
            Items = new ObservableCollection<string>();
            this.connectivity = connectivity;
        }

        [ObservableProperty]
        private ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [RelayCommand]
        async void Add()
        {

            if (string.IsNullOrEmpty(Text))
                return;
            if(connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("OH OH!", "No Internet", "OK");
                return;
            }
            
            Items.Add(Text);
            //add our item
            Text = string.Empty;
        }

        [RelayCommand]
        void Delete(string s)
        {
            if (Items.Contains(s))
            {
                Items.Remove(s);
            }
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
                //new Dictionary<string, object>
                //{
                //    {nameof(DetailPage), new object() },
                //});
        }

        //    public string Text
        //    {
        //        get => text;
        //        set 
        //        {
        //            text = value;
        //            OnPropertyChanged(nameof(Text));
        //        } 
        //    }

        //    public event PropertyChangedEventHandler PropertyChanged;



        //    void OnPropertyChanged(string name) =>
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
