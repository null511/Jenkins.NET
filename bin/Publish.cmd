if exist "Jenkins.NET\bin\Package\" rmdir /Q /S "Jenkins.NET\bin\Package"
nuget pack "Jenkins.NET\Jenkins.NET.csproj" -OutputDirectory "Jenkins.NET\bin\Package" -Build -Prop "Configuration=Release;Platform=AnyCPU"
nuget push "Jenkins.NET\bin\Package\*.nupkg" -Source "https://www.nuget.org/api/v2/package" -NonInteractive
