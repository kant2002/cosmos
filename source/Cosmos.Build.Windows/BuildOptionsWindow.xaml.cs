﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cosmos.Build.Windows {
    /// <summary>
    /// Interaction logic for BuildOptionsWindow.xaml
    /// </summary>
    public partial class BuildOptionsWindow : Window {
        public BuildOptionsWindow() {
            InitializeComponent();
            foreach (var xTarget in Enum.GetNames(typeof(Builder.Target))) {
                lboxTargets.Items.Add(xTarget);
            }
        }
    }
}
