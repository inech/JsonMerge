# JsonMerge

This simple tool is just a sample of using [System.CommandLine](https://github.com/dotnet/command-line-api/) API.

## Features

- Merge multiple JSON files into one.
- Meaningful error messages when incorrect command line arguments were provided.
- Tab completion for command line arguments.

## Installation

1. Ensure you have .NET Core SDK 3.0 installed ([download](https://dotnet.microsoft.com/download/dotnet-core/3.0)).
1. Install it as a [.NET Core global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools):
    - (Option 1) From nuget.org: `dotnet tool install --global JsonMerge.CommandLine`
    - (Option 2) Or build & install: 
      - `cd src/JsonMerge.CommandLine`
      - `dotnet pack`
      - `dotnet tool install --global --add-source ./nupkg JsonMerge.CommandLine`
1. (Bonus) If you want to have tab completion you need to install `dotnet-suggest` as described [here](https://github.com/dotnet/command-line-api/wiki/dotnet-suggest).

## Usage

```console
> jsonmerge --help

jsonmerge:
  Merge json files

Usage:
  jsonmerge [options] <input-files>...

Arguments:
  <input-files>    Two or more JSON files to be merged

Options:
  -o, --output, --output-file <o>    Output file path
  -f, --force                        Override output file if it already exists
  --version                          Display version information
```

## Example

Suppose you want to merge 2 json files

### sampleData\1.json

```json
{
    "Name": "John",
    "Properties": {
        "HairColor": "Black"
    }
}
```
### sampleData\2.json

```json
{
    "LastName": "Snow",
    "Properties": {
        "HairColor": "Blond",
        "Knows": "Nothing"
    }
}
```
To do this, just run `jsonmerge`

```console
> jsonmerge sampleData\1.json sampleData\2.json -o sampleData\results\merged.json -f
```
and you will get the merged result
### sampleData\results\merged.json

```json
{
  "Name": "John",
  "Properties": {
    "HairColor": "Blond",
    "Knows": "Nothing"
  },
  "LastName": "Snow"
}
```