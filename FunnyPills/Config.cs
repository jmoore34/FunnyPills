﻿using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCPReplacer
{
    public class Config : IConfig
    {
        public bool Debug { get; set; } = false;


        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
    }
}