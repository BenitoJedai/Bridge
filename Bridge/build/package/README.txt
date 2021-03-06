﻿Project          : Bridge
Release Date     : 2015-03-30
Current Version  : 1.0.0

Use Bridge.NET to build platform independent applications for mobile, web 
and desktop. Run on iOS, Windows, Mac, Linux and billions of other devices 
with JavaScript support.

The foundation of Bridge.NET is a C# to JavaScript source-to-source compiler, 
also known as a Cross Compiler or Transpiler. Using Bridge.NET you write your 
code in C# and Bridge.NET will magically transform into JavaScript. 

Please visit the Bridge.NET website (http://bridge.net/) for more information.

--------------------------------------------------------------------------
CONTENTS
--------------------------------------------------------------------------

I.   SYSTEM REQUIREMENTS
II.  INSTALLATION INSTRUCTIONS
III. REVISIONS + BREAKING CHANGES
IV.  CREDITS


--------------------------------------------------------------------------
I. SYSTEM REQUIREMENTS
--------------------------------------------------------------------------

1. Visual Studio 2012, 2013, and 2015
3. .NET Framework 4.5 and higher


--------------------------------------------------------------------------
II. INSTALLATION INSTRUCTIONS
--------------------------------------------------------------------------

The Getting Started Knowledge Base article is a great place to start:
    http://bridge.net/kb/getting-started


Or, jump right in and download the Visual Studio template installer from 
the Download page:
    http://bridge.net/download/


Another equally fast installation option is using NuGet to install 
directly into a new C# Class Library project. Use any of the following 
commands on the Package Manager Console to quickly install all the 
necessary pieces and setup your project for Bridge.


    PM> Install-Package Bridge


jQuery and Bootstrap packages are also available to install as per your 
project requirements:

    PM> Install-Package Bridge.jQuery

    PM> Install-Package Bridge.Bootstrap


--------------------------------------------------------------------------
III. REVISIONS + BREAKING CHANGES
--------------------------------------------------------------------------

Initial Release. No BREAKING CHANGES.


--------------------------------------------------------------------------
VII. CREDITS
--------------------------------------------------------------------------

1.  NRefactory by  ICSharpCode

    https://www.nuget.org/packages/ICSharpCode.NRefactory

2.  Mono.Cecil by Jb Evain

    https://www.nuget.org/packages/Mono.Cecil

3.  Json.NET by James Newton-King
    
    https://www.nuget.org/packages/Newtonsoft.Json

4.  AjaxMin by Microsoft

    https://www.nuget.org/packages/AjaxMin
    
--------------------------------------------------------------------------

    Copyright 2008-2015 Object.NET, Inc., All rights reserved.
                  
                      Object.NET, Inc.
                      +1(888)775-5888
                      hello@object.net
                         object.net