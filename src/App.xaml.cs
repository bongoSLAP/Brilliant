﻿using Microsoft.Maui.Controls;

namespace Brilliant;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
