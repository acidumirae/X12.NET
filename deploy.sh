ApiKey=$1
Source=$2
Version="5.0.${3}"

# List all packages with nuspecs to be packed and pushed
nuget pack ./src/X12.Core.Hipaa/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Parsing/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Shared/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Specifications/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Sql/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Validation/package.nuspec -Version $Version -Verbosity detailed

nuget setApiKey $ApiKey -Source $Source -Verbosity detailed

nuget push X12.*.nupkg -Source $Source
