#!/bin/bash

mono "./bin/NuGet.exe" restore "./Jenkins.NET.sln"

msbuild "./Jenkins.NET.Publishing/Jenkins.NET.Publishing.csproj" /t:Rebuild /p:Configuration="Debug" /p:Platform="Any CPU" /p:OutputPath="bin\Debug" /v:m
