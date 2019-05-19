[![Build status](https://ci.appveyor.com/api/projects/status/6o3es3fa5fxpvf2v?svg=true)](https://ci.appveyor.com/project/monkog/binary-files-reader)

## Binary Files Reader
This program allows you to read an **.exe* or **.dll* file and list objects that are compiled into this file.

![Application screenshot](./.Docs/Application.JPG)

The list on the left contains a tree structure of files that were opened. It lists:
* namespaces
* classes
* interfaces
* enums

The list on the right appears after choosing a certain class or interface. It lists objects that are defined in the chosen type such as:
* constants
* fields
* properties
* methods
* events

### Creating and manipulating an instance of an object
If the type provides a public parameterless constructor, the application gives you a possibility to create an instance of this type.  

![Field value window screenshot](./.Docs/FieldValue.JPG)

If an instance of a type is available, it's possible to see values that are assigned to fields by double clicking on them.

![Invoke method window screenshot](./.Docs/MethodInvoke.JPG)

It's also possible to invoke methods that require no or just simple type parameters.
