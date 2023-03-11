ApiKey=$1
Source=$2

# Defining the version here for consistent versioning across packages, may segregate later on
Version="5.0.${3}"

# Core packages that need to be packed prior to any other packages
nuget pack ./src/X12.Core.Specifications/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Shared/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Transformations/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Parsing/package.nuspec -Version $Version -Verbosity detailed

# Supplemental packages to pack
nuget pack ./src/X12.Core.Validation/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Sql/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Core.Hipaa/package.nuspec -Version $Version -Verbosity detailed

# Publish the packages - breaking list down to sub-packages due to a deployment issue
nuget setApiKey $ApiKey -Source $Source -Verbosity detailed

nuget push X12.Core.Hipaa.*.nupkg -Source $Source
nuget push X12.Core.Parsing.*.nupkg -Source $Source
nuget push X12.Core.Shared.*.nupkg -Source $Source
nuget push X12.Core.Specifications.*.nupkg -Source $Source
nuget push X12.Core.Sql.*.nupkg -Source $Source
nuget push X12.Core.Transformations.*.nupkg -Source $Source
nuget push X12.Core.Validation.*.nupkg -Source $Source
