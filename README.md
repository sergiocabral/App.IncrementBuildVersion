# App.IncrementBuildVersion

Este breve código C# serve para gerar um aplicativo que incrementa o número de build da versão em um arquivo .csproj.

Por exemplo, o código abaixo é de um arquivo de projeto do .NET Core. É indicado a versão **2.0.0.213**.

Foi registrado um evento pós-build, `PostBuildEvent`, que executa o aplicativo `IncrementBuildVersion.exe` que garante o incremento da versão para **2.0.0.214** após o próximo build.

```
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <Version>2.0.0.213</Version>
    </PropertyGroup>

    <Target Name="PostBuild" AfterTargets="PostBuildEvent">
        <Exec Command="..\IncrementBuildVersion.exe" />
    </Target>
    
</Project>
```

Nativamente o Windows já vem com um compilador C#. Para gerar o aplicativo use o comando abaixo, chamando o compilador C# `csc.exe` passando como parâmetro o código fonte no arquivo `Program.cs`:
- `C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe Program.cs`
