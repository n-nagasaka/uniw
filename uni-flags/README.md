# uni-flags

C# コマンドラインパーサー

## SPECS

フラグを解析

`-f` 短い形式のフラグ
`--flag` 長い形式のフラグ

オプションを解析
`-o <option-value>` 短い形式のオプション
`--option <opition-value>` 長い形式のオプション

Usage 表示

```
string usage = flags.Usage()
```

Help 表示
`--help` か `-h` でヘルプを表示

```csharp

```