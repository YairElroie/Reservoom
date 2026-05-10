using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.DbContexts;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationProviders;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;

namespace Reservoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private const string CONNECTION_STRING = "Data Source=reservoom.db";
        //private readonly Hotel _hotel;
        private readonly IHost _host;
        //private readonly HotelStore _hotelStore;
        //private readonly NavigationStore _navigationStore;
        //private ReservoomDbContextFactory _reservoomDbContextFactory;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
            {
                string connectinString = hostContext.Configuration.GetConnectionString("Default");

                services.AddSingleton(new ReservoomDbContextFactory(connectinString));
                services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                services.AddTransient<ReservationBook>();
                services.AddSingleton((s) => new Hotel("Yair Hotel", s.GetRequiredService<ReservationBook>()));

                services.AddTransient((s) => CreateReservationListingViewModel(s));
                services.AddSingleton<Func<ReservationListingViewModel>>(s => () => s.GetRequiredService<ReservationListingViewModel>());
                services.AddSingleton<NavigationService<ReservationListingViewModel>>();

                services.AddTransient<MakeReservationViewModel>();
                services.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());
                services.AddSingleton<NavigationService<MakeReservationViewModel>>();

                services.AddSingleton<HotelStore>();
                services.AddSingleton<NavigationStore>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });

            }).Build();

            //_reservoomDbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);
            //IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
            //IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
            //IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory);
            //ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
            //_hotel = new Hotel("Yair Hotel", reservationBook);
            //_hotelStore = new HotelStore(_hotel);
            //_navigationStore = new NavigationStore();

            //_hotelStore = new HotelStore(_hotel);
            //_navigationStore = new NavigationStore();
        }

        private ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        {
            return ReservationListingViewModel.LoadViewModel(
                services.GetRequiredService<HotelStore>(),
                services.GetRequiredService<NavigationService<MakeReservationViewModel>>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            //DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;

            ReservoomDbContextFactory reservoomDbContextFactory = _host.Services.GetRequiredService<ReservoomDbContextFactory>();
            //using (ReservoomDbContext dbContext = _reservoomDbContextFactory.CreateDbContext())
            using (ReservoomDbContext dbContext = reservoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            //NavigationStore navigationStore = _host.Services.GetRequiredService<NavigationStore>();
            //navigationStore.CurrentViewModel = CreateReservationListingViewModel();
            //_navigationStore.CurrentViewModel = CreateReservationListingViewModel();

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainViewModel(_navigationStore)
            //};

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        //private MakeReservationViewModel CreateMakeReservationViewModel()
        //{
        //    return new MakeReservationViewModel(_hotelStore, new Services.NavigationService(_navigationStore, CreateReservationListingViewModel));
        //}

        //private ReservationListingViewModel CreateReservationListingViewModel()
        //{
        //    return ReservationListingViewModel.LoadViewModel(_hotelStore, CreateMakeReservationViewModel(), new Services.NavigationService(_navigationStore, CreateMakeReservationViewModel));
        //}

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
