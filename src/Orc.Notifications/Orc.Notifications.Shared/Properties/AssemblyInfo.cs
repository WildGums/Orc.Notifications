﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// All other assembly info is defined in SharedAssembly.cs

[assembly: AssemblyTitle("Orc.Notifications")]
[assembly: AssemblyProduct("Orc.Notifications")]
[assembly: AssemblyDescription("Orc.Notifications library")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

#if DEBUG
[assembly: InternalsVisibleTo("Orc.Notifications.Tests")]
#endif