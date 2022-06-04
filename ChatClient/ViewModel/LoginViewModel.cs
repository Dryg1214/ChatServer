﻿using ChatClient.Command;
using ChatClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatClient.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private IChatService chatService;

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private string? _userName;
        public string? UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        #region Connect Command
        private ICommand ?_connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new RelayCommandAsync(Connect));
            }
        }
        private async Task<bool> Connect()
        {
            try
            {
                await chatService.Connect();
                IsConnected = true;
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion


        private ICommand ?_loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand =
                    new RelayCommandAsync(Login));
            }
        }
        private async Task<bool> Login()
        {
            try
            {   
                if (IsConnected == false)
                {
                    MessageBox.Show("You dont connect to a server");
                    return false;
                }
                var service = new ChatService();
                var mainViewModel = new MainViewModel(service);
                if (await service.Login(_userName))
                {
                    new MainWindow { DataContext = mainViewModel }.Show();
                }
                else
                {
                    MessageBox.Show("This Login already exists");
                    return false;
                }

                return true;
            }
            catch (Exception e) 
            { 
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}