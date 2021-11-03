# regextrim
Trim strings in a given text file using a regex pattern

## Usage
```
regextrim -regex regex -file file [-ignorecase] [-out file]
```
Where,

* regex - The Regular expression to use and fine the part of the string to trim.
* file - Fully qualified path to the file containing strings to trim.
* ignorecase - When set, the regex search will ignore character casing.
* out - Optional path to a file where trimmed strings are written to. By default output is written to the console.

