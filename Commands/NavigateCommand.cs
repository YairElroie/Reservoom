using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Commands
{
    public class NavigateCommand<TViewModle> : CommandBase where TViewModle : ViewModelBase
    {
        private readonly NavigationService<TViewModle> _navigationService;

        public NavigateCommand(NavigationService<TViewModle> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
