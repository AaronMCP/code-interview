using System;
using System.Reflection;
using System.Security.Permissions;
using System.Runtime.InteropServices;

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.0.0.9")]
[assembly: AssemblyFileVersion("1.0.0.9")]

//
// FxCop requires the following attributes
//
[assembly: FileIOPermission(SecurityAction.RequestMinimum)]

// We are lying about CLS Compliance, because using System.Data is not CLS Compliant.
// Still, setting true allows Compliant parts of the assembly to be re-used.
// And this removes the FxCop warnings.
[assembly: CLSCompliant(true)]

//
// General Common Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyCompany("Haoyisheng")]
[assembly: AssemblyProduct("Hys.Platform")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
	


