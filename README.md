# JsonMerge

JsonMerge is a small command line utility that merges multiple JSON files into a single document. It targets **.NET 8** and can be installed as a global tool or run locally from source.

## Building

```bash
dotnet build
```

## Installation

```
dotnet tool install --global --add-source ./nupkg JsonMerge.CommandLine
```

## Usage

```bash
jsonmerge <input1> <input2> [<inputN>...] -o <output.json> [-f]
```

* `-o`, `--output`  - path to the resulting JSON file (required)
* `-f`, `--force`   - overwrite the output file if it already exists

## Example

Input files:

### sampleData/1.json
```json
{
    "Name": "John",
    "Properties": {
        "HairColor": "Black"
    }
}
```

### sampleData/2.json
```json
{
    "LastName": "Snow",
    "Properties": {
        "HairColor": "Brown",
        "Knows": "Nothing"
    }
}
```

Run the command:

```bash
jsonmerge sampleData/1.json sampleData/2.json -o merged.json -f
```

Produces `merged.json`:

```json
{
  "Name": "John",
  "Properties": {
    "HairColor": "Brown",
    "Knows": "Nothing"
  },
  "LastName": "Snow"
}
```
