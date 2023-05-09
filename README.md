
# Setup

Simple application which opens and executes .bat files with admin's rights.




## Usage/Examples

- name of the .bat file is hardcoded as setup.exe is supposed to be used in environment where user should not have an access to modify .exe. you can modify filename in code above
```csharp
    else
    {
        var filename = "keys.bat";
        var fullPath = Path.GetFullPath(filename);
        var psi = new ProcessStartInfo();
    ...
```
- .bat and .exe file must be in the same location 


## Tech Stack

**setup.exe** .NET Framework 4.7.2


## Authors

- [@Błażej Furgała](https://github.com/B1a7)

