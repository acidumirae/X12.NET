ApiKey=$1
Source=$2

# List all packages with nuspecs to be packed and pushed
nuget pack ./src/X12.Core.Hipaa/package.nuspec
nuget pack ./src/X12.Core.Parsing/package.nuspec
nuget pack ./src/X12.Core.Shared/package.nuspec
nuget pack ./src/X12.Core.Specifications/package.nuspec
nuget pack ./src/X12.Core.Sql/package.nuspec
nuget pack ./src/X12.Core.Validation/package.nuspec

nuget setApiKey $ApiKey -Source $Source -Verbosity detailed

nuget push X12.*.nupkg -Source $Source
