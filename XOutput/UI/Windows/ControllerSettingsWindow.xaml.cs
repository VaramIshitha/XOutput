﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using XOutput.Devices;
using XOutput.Tools;

namespace XOutput.UI.Windows
{
    public partial class ControllerSettingsWindow : WindowBase<ControllerSettingsViewModel, ControllerSettingsModel>
    {
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private GameController controller;

        [ResolverMethod(Scope.Prototype)]
        public ControllerSettingsWindow(ControllerSettingsViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        public void Initialize(GameController controller)
        {
            this.controller = controller;
            ViewModel.Initialize(controller);
        }

        public override void CleanUp()
        {
            timer.Tick -= TimerTick;
            timer.Stop();
            ViewModel.CleanUp();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Update();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ViewModel.Update();
        }

        private void ConfigureAllButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ConfigureAll();
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ViewModel.SetStartWhenConnected();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SetForceFeedback();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            controller.Mapper.Name = ViewModel.Model.Title;
        }
    }
}
