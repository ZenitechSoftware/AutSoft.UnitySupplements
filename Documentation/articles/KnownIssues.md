# Known issues

## 0.1.0 - *

- Duplicate file names in the same class module will break the generated file. The solution for this requires further investigation

## 0.1.0 - 0.2.0: Fixed in 0.3.0

- SpecialCharacters can break generated names.
  - Numbers at the end of the files are supported, but not at the start
  - Any character in the filename which would not be a valid C# filed/property/method name will break the generated code
  - Spaces get removed
  - For Example:
    - `Coin1` works
    - `Coin 1` works
    - `1Coin` does not
    - `!Coin` does not
    - `Coin!` does not
