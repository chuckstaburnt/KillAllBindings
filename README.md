# KillAllBindings
KillAllBindings is a tool that removes TFS source control bindings from a project written in C#.

### Objective
This was written specifically for use when migrating source code from TFS to another SCM. If these bindings are left in the solution files, Visual Studio will try to connect to your TFS server again. This can cause issues.

I wrote this in preparation for a migration from TFS to Stash (commercial version of Git) but it should work regardless of what SCM you're moving to.

### Versions Tested
This has been tested on VS2010 and VS2013 solution files. 

### How It Works
KillAllBindings only does a few things.
- Remove read only flags from ALL files in your project.
- Delete .vssscc & vspscc files.
- Remove source code bindings section from solution files (if present).

### Download
If you do not wish to download the source and build it yourself, you can download the latest executable from here: http://chasberndt.com/kab/latest/

### Usage
````
KillAllBindings.exe C:\source\code\path\
````

### Disclaimer
First and most importantly, this is the <b>first</b> thing I've ever written in C#. Regardless, I am not responsible for any and all issues that arise out of the use of this code.

Feel free to contribute to this project, submit bugs, fix bugs, whatever. I want this to be as useful as possible.

### Version
<b>1.4</b>
- Now expects an argument at runtime for the source code path.
- Prints out slightly less crap.

<b>1.3</b>
- vspscc files are now removed.

<b>1.2</b>
- Change some of the language in the script to be more clear.
- Added a "recap" section at the bottom of the script since results get truncated on large filesets.

<b>1.1</b>
- Added an extra check before continuing.

<b>1.0</b>
- Initial release.

### License
The MIT License (MIT)

Copyright (c) 2015-2016 Chas Berndt

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.